using System;
using System.Collections.Generic;


namespace Juego
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ana Lucía Chávez - Proyecto 2");
            Console.WriteLine("------------------------------");
            Console.WriteLine("CONNECT 4");
            Console.WriteLine("-----------------------------------------------------------");

            //Elegir el modo de juego
            Console.WriteLine("¿Desean jugar dos personas o una persona contra la computadora?");
            Console.WriteLine(" ");
            Console.WriteLine("Ingrese '1' jugar dos personas o '2' para jugar contra la computadora");
            int numJugadores = Convert.ToInt32(Console.ReadLine());
            string jugador1, jugador2;
            if (numJugadores == 2)
            {
                jugador1 = "Jugador";
                jugador2 = "COMPUTADORA";
            }
            else
            {
                Console.WriteLine("Ingrese el nombre del jugador 1");
                jugador1 = Console.ReadLine();
                Console.WriteLine("Ingrese el nombre del jugador 2");
                jugador2 = Console.ReadLine();

                //verificar que ningun jugador tenga el mismo nombre o se llame COMPUTADORA
                while (jugador1 == "COMPUTADORA" || jugador2 == "COMPUTADORA" || jugador1 == jugador2)
                {
                    Console.WriteLine("ERROR. El nombre no puede ser 'COMPUTADORA'");
                    Console.WriteLine("Ingrese el nombre del primer jugador");
                    jugador1 = Console.ReadLine();
                    Console.WriteLine("Ingrese el nombre del segundo jugador");
                    jugador2 = Console.ReadLine();
                }
            }

            //crear tablero de juego
            int[,] tablero = new int[6, 7];
            bool turnoJugador1 = true;
            bool juegoActivo = true;

            //inicio del juego
            while (juegoActivo)
            {
                int columna;
                if (turnoJugador1)
                {
                    Console.WriteLine("Que empiece el juego entre  " + jugador1 + " y " + jugador2);
                    Console.WriteLine(" ");
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine(jugador1 + ", es tu turno, elige una columna donde desees colocar tu ficha");
                    columna = Convert.ToInt32(Console.ReadLine()) - 1;

                    //Verificar que la columna que selecciono el usuario si esta disponible
                    while (columna < 0 || columna > 6 || tablero[0, columna] != 0)
                    {
                        Console.WriteLine("Inválido. Intenta de nuevo");
                        columna = Convert.ToInt32(Console.ReadLine()) - 1;
                    }
                    int fila = 5;

                    //encontrar la primera fila disponible en la columna que se selecciono
                    while (tablero[fila, columna] != 0)
                    {
                        fila--;
                    }
                    tablero[fila, columna] = 1;
                }
                else
                {
                    Console.WriteLine(jugador2 + ", es tu turno, elige una columna donde desees colocar tu ficha");
                    if (jugador2 == "COMPUTADORA")
                    {
                        Random rnd = new Random();
                        columna = rnd.Next(0, 7);

                        //encontrar una columna aleatoria en el tablero donde la computadora pueda colocar su ficha
                        while (tablero[0, columna] != 0)
                        {
                            columna = rnd.Next(0, 7);
                        }
                        int fila = 5;

                        //mostrar un mensaje en la consola para indicar en qué columna colocó su ficha
                        while (tablero[fila, columna] != 0)
                        {
                            fila--;
                        }
                        tablero[fila, columna] = 2;
                        Console.WriteLine("-----------------------------------------------------------");
                        Console.WriteLine("La COMPUTADORA colocó su ficha en la columna 3" + (columna + 1));
                        Console.WriteLine(" ");
                    }

                    //pedir al usuario que ingrese la columna en la que quiere colocar su ficha
                    else
                    {
                        columna = Convert.ToInt32(Console.ReadLine()) - 1;
                        while (columna < 0 || columna > 6 || tablero[0, columna] != 0)
                        {
                            Console.WriteLine("Inválido. Intenta de nuevo");
                            columna = Convert.ToInt32(Console.ReadLine()) - 1;
                        }
                        int fila = 5;

                        //colocar la ficha del usuario e imprimir el tablero de juego actualizado
                        while (tablero[fila, columna] != 0)
                        {
                            fila--;
                        }
                        tablero[fila, columna] = 2;
                    }
                }
                ImprimirTablero(tablero);

                //indicar quien gano la partida o si fue un empate  y llamar a las funciones indicadas
                if (Ganador(tablero, 1))
                {
                    Console.WriteLine(jugador1 + " ganó la partida!");
                    juegoActivo = false;
                    RegistrarGanador(jugador1);
                    ImprimirUltimosGanadores();
                }
                else if (Ganador(tablero, 2))
                {
                    Console.WriteLine(jugador2 + " ganó la partida!");
                    juegoActivo = false;
                    RegistrarGanador(jugador2);
                    ImprimirUltimosGanadores();
                }
                else if (Empate(tablero))
                {
                    Console.WriteLine("Empate!");
                    juegoActivo = false;
                }
                else
                {
                    turnoJugador1 = !turnoJugador1;
                }
            }

            //seleccionar si quiere seguir jugando o salirse
            Console.WriteLine("¿Desea jugar de nuevo?");
            Console.WriteLine("Ingrese '1' para jugar de nuevo o '2' para salir");
            int opcion = Convert.ToInt32(Console.ReadLine());
            if (opcion == 1)
            {
                Main(args);
            }
            else
            {
                Console.WriteLine("Fin del juego");
            }
        }

        //verificar si estan las 4 fichas consecutivas
        static bool Ganador(int[,] tablero, int jugador)
        {
            // Verificar las filas
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (tablero[i, j] == jugador && tablero[i, j + 1] == jugador && tablero[i, j + 2] == jugador && tablero[i, j + 3] == jugador)
                    {
                        return true;
                    }
                }
            }
            // Verificar las columnas
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (tablero[i, j] == jugador && tablero[i + 1, j] == jugador && tablero[i + 2, j] == jugador && tablero[i + 3, j] == jugador)
                    {
                        return true;
                    }
                }
            }
            // Verificar las diagonales
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (tablero[i, j] == jugador && tablero[i + 1, j + 1] == jugador && tablero[i + 2, j + 2] == jugador && tablero[i + 3, j + 3] == jugador)
                    {
                        return true;
                    }
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 3; j < 7; j++)
                {
                    if (tablero[i, j] == jugador && tablero[i + 1, j - 1] == jugador && tablero[i + 2, j - 2] == jugador && tablero[i + 3, j - 3] == jugador)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static bool Empate(int[,] tablero)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (tablero[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //Imprimir en la consola el estado actual del tablero 
        static void ImprimirTablero(int[,] tablero)
        {
            Console.WriteLine(" 1 2 3 4 5 6 7");
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {

                    //Diseño del tablero y variables para los jugadores
                    Console.Write("|");
                    if (tablero[i, j] == 0)
                    {
                        Console.Write(" ");
                    }
                    else if (tablero[i, j] == 1)
                    {
                        Console.Write("O");
                    }
                    else
                    {
                        Console.Write("X");
                    }
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("---------------");
        }

        //registrar los ganadores
        static void RegistrarGanador(string jugador)
        {
            List<string> ganadores = new List<string>();
            if (System.IO.File.Exists("ganadores.txt"))
            {
                string[] lines = System.IO.File.ReadAllLines("ganadores.txt");
                foreach (string line in lines)
                {
                    ganadores.Add(line);
                }
            }
            ganadores.Add(DateTime.Now.ToString() + " - " + jugador);
            if (ganadores.Count > 10)
            {
                ganadores.RemoveAt(0);
            }
            System.IO.File.WriteAllLines("ganadores.txt", ganadores.ToArray());
        }

        //imprimir los resultados finales del juego
        static void ImprimirUltimosGanadores()
        {
            Console.WriteLine("Últimos 10 ganadores: ");
            if (System.IO.File.Exists("ganadores.txt"))
            {
                string[] lines = System.IO.File.ReadAllLines("ganadores.txt");
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}