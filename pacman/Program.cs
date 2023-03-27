﻿using System;
using System.IO;
using System.Threading;

namespace pacman
{
    internal class Program
    {
        //
        static void Main(string[] args)
        {

            Console.CursorVisible = false;

            char[,] map = ReadMap("map.txt");
            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);

            Task.Run(() =>
            {
                while (true)
                {
                    pressedKey = Console.ReadKey();
                }
            });


            int pacmanX = 1, pacmanY = 1;
            int score = 0;

            while (true)
            {



                Console.Clear();

                HandleInput(pressedKey, ref pacmanX, ref pacmanY, map, ref score);

                Console.ForegroundColor = ConsoleColor.Blue;
                DrowMap(map);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(pacmanX, pacmanY);
                System.Console.Write("@");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(32, 0);
                Console.Write($"Score: {score}");

                Thread.Sleep(1000);


            }

            // string[] file = File.ReadAllLines("map.txt");

            // System.Console.WriteLine(file[0]);


            /// конец main
        }




        private static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines("map.txt");

            char[,] map = new char[GetMapsLenghtsOfLine(file), file.Length];

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = file[y][x];
                }
            }
            return map;
        }

        private static int GetMapsLenghtsOfLine(string[] lines)
        {
            int maxLength = lines[0].Length;

            foreach (var line in lines)
                if (line.Length > maxLength)
                    maxLength = line.Length;
            return maxLength;
        }

        private static void DrowMap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    System.Console.Write(map[x, y]);
                }
                System.Console.Write("\n");
            }

        }

        private static void HandleInput(ConsoleKeyInfo pressedKey, ref int pacmanX, ref int pacmanY, char[,] map, ref int score)
        {
            int[] direction = GetDerection(pressedKey);

            int nextPacmanPositionX = pacmanX + direction[0];
            int nextPacmanPositionY = pacmanY + direction[1];

            char nextCell = map[nextPacmanPositionX, nextPacmanPositionY];

            if (nextCell == ' ' || nextCell == '.')
            {
                pacmanX = nextPacmanPositionX;
                pacmanY = nextPacmanPositionY;

                if (nextCell == '.')
                {
                    score++;
                    map[nextPacmanPositionX, nextPacmanPositionY] = ' ';
                }
            }
        }

        private static int[] GetDerection(ConsoleKeyInfo pressedKey)
        {
            int[] direction = { 0, 0 };

            if (pressedKey.Key == ConsoleKey.UpArrow)
            {
                direction[1] = -1;
            }
            if (pressedKey.Key == ConsoleKey.DownArrow)
            {
                direction[1] = 1;
            }
            if (pressedKey.Key == ConsoleKey.LeftArrow)
            {
                direction[0] = -1;
            }
            if (pressedKey.Key == ConsoleKey.RightArrow)
            {
                direction[0] = 1;
            }
            return direction;
        }


    }
}
