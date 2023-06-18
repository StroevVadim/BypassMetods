using System;

namespace Bypass_Metods
{
    /* Данный класс содержит в себе различные методы для обхода двумерных массивов данных.
     * На данный момент данный класс имеет следующие методы:
     * Spiral — Обходит массив по спирали внутрь.
     */
    class BypassMetods
    {
        /* Структура представляющая собой
         * координаты точки по осям X и Y.
         * Так же могут обозначать вектор.
         */
    private struct Point
        {
            public int x, y;

            public Point(int x = 0, int y = 0) 
            {
                this.x = x;
                this.y = y;
            }

            public void Set(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        /* Функция, которая принимает ссылки
         * на параметры X и Y как вектор
         * и осущствляет его поворот на 90 граудсов.
         * Направление врещение определяется переменной direction.
         * direction = 1 — против часовой, direction = -1 — по часовой.
         */
        private static void Rotatete(ref int x, ref int y, int direction)
        {
            int R_x = x, R_y = y;
            x = 0 * R_x + (-1) * direction * R_y;
            y = direction * R_x + 0 * R_y;
        }

        /* Функция развертывания матрицы в одномерный массив.
         * Принимает на вход сам массив (massiv) и указания по его обходу.
         * Направление обхода опредялется direction. 
         * direction = 1 — против часовой, direction = -1 — по часовой.
         * Индекс по рядам обозначается через x (int[x,]).
         * Индекс по столбцам обозначается через y (int[,y]).
         * Вектор стартового направления обхода определяется переменными V_x и V_y.
         * Индекс стартового положения обхода задается S_x и S_y.
         * Размер области для обхода определяется параметрами lenght (по оси x) и width (по оси y).
         * Исходя из всех параметров опредялется направление и зона обхода.
         * По умолчанию обход против часовой стрелки всего массива из стартового положения массива в нижнем направлении.
         */
        public static int[] Spiral(int[,] massiv, int direction = 1,
            int V_x = 1, int V_y = 0,
            int S_x = 0, int S_y = 0, int lenght = 0, int width = 0)
        {
            /* Обрабатываем входные данные и на основании их
             * определяем рабочую область обхода спиралью.
             * Если размер области не указан (lenght/width == 0), 
             * то в качестве параметров берем размеры массива.
             */
            if (lenght == 0) lenght = massiv.GetLength(0);
            if (width == 0) width = massiv.GetLength(1);
            int E_x = 0, E_y = 0;                   //  Противоположный угол от стратового на оснвоании размеров
            Point A = new Point(V_x, V_y);          //  Вектор содержащий направление обхода массива
            Rotatete(ref A.x, ref A.y, direction);  //  Определение вектора обхода
            Point last_angle = new Point(0, 0);     //  Угол, с которого начинается обход массива спиралью 
            if ((A.x == -1) || (V_x == -1)) last_angle.x = 1;
            if ((A.y == -1) || (V_y == -1)) last_angle.y = 1;
            E_x = S_x + (V_x + A.x) * Math.Abs(lenght);
            E_y = S_y + (V_y + A.y) * Math.Abs(width);

            /* Если значения выходят за пределы массива, 
             * то их приравниваем к ближайшей границе.
             */
            {
                if (E_x < 0) E_x = 0;
                if (E_y < 0) E_y = 0;
                if (E_x >= massiv.GetLength(0)) E_x = massiv.GetLength(0) - 1;
                if (E_y >= massiv.GetLength(1)) E_y = massiv.GetLength(1) - 1;
            }

            /* Определение углов зоны обхода спиралькой.
             * Угол [0, 0] — верхний левый угол зоны.
             * Угол [1, 0] — нижний левый угол зоны.
             * Угол [0, 1] — верхний правый угол зоны.
             * Угол [1, 1] — нижний правый угол зоны.
             */
            Point[,] Angles = new Point[2, 2];  
            Angles[0, 0].Set(Math.Min(S_x, E_x), Math.Min(S_y, E_y));
            Angles[1, 0].Set(Math.Max(S_x, E_x), Math.Min(S_y, E_y));
            Angles[1, 1].Set(Math.Max(S_x, E_x), Math.Max(S_y, E_y));
            Angles[0, 1].Set(Math.Min(S_x, E_x), Math.Max(S_y, E_y));

            /*  Дальше происход обход массива спиралью и копирование занчений в однорядный массив.
             *  Происход смещение индексов в направлении вектора обхода.
             *  Когда указатель обхода дойдет до угла, в который он направлен, 
             *  произойдет смена направления обхода и смещение границы области обхода.
             *  Таким образом происходит обход несколько раз сужая границы обхода.
             */
            int Size = (Angles[1, 1].x - Angles[0, 0].x + 1) 
                * (Angles[1, 1].y - Angles[0, 0].y + 1);    //  Размер одномерного массива
            int[] result = new int[Size];                   //  Одномерный массив
            int x = S_x, y = S_y;                           //  Индексы двумерного массива для обхода
            Point new_angle = new Point(last_angle.x, last_angle.y);    //  Новый угол обхода
            for (int i = 0; i < Size; i++)
            {
                result[i] = massiv[x, y];
                x += V_x;
                y += V_y;
                if ((x == Angles[V_x + last_angle.x, V_y + last_angle.y].x)
                    && (y == Angles[V_x + last_angle.x, V_y + last_angle.y].y))
                {
                    new_angle.x += V_x;
                    new_angle.y += V_y;
                    Rotatete(ref V_x, ref V_y, direction);

                    Angles[last_angle.x, last_angle.y].x += V_x;
                    Angles[new_angle.x, new_angle.y].x += V_x;

                    Angles[last_angle.x, last_angle.y].y += V_y;
                    Angles[new_angle.x, new_angle.y].y += V_y;

                    last_angle.Set(new_angle.x, new_angle.y);
                }
            }
            
            return result;
        }
    }
}