using System;
using System.Collections.Generic;//con esto le dice a C# que vas a usar listas y diccionarios, si no el programa no va a saber que son y dara error

namespace SistemaRestaurantes_Senator
{
    class Program
    {
        static Dictionary<string, int> capacidades = new Dictionary<string, int>()
        {
            { "Ember", 3 }, { "Zao", 4 }, { "Grappa", 2 }, { "Larimar", 3 }
        };

        static Dictionary<string, Dictionary<string, List<(string nombre, int personas)>>> reservaciones
            = new Dictionary<string, Dictionary<string, List<(string, int)>>>();

        static void Main(string[] args)
        {
            // Crea las listas vacias para cada restaurante y turno
            foreach (var r in capacidades.Keys)
                reservaciones[r] = new Dictionary<string, List<(string, int)>>()
                {
                    { "TurnoA", new List<(string, int)>() },
                    { "TurnoB", new List<(string, int)>() }
                };

            int opcion = 0;
            while (opcion != 5)
            {
                MostrarMenu();
                if (!int.TryParse(Console.ReadLine(), out opcion)) opcion = 0;

                if      (opcion == 1) RealizarReservacion();
                else if (opcion == 2) EliminarReserva();
                else if (opcion == 3) VerDisponibilidad();
                else if (opcion == 4) ImprimirListado();
                else if (opcion == 5) Console.WriteLine("Cerrando sistema...");
                else                  Console.WriteLine("Opcion no valida.");
            }
        }


        static void MostrarMenu()
        {
            Console.WriteLine("\n===== Sistema Senator =====");
            Console.WriteLine("1. Realizar Reservacion");
            Console.WriteLine("2. Eliminar Reserva");
            Console.WriteLine("3. Ver Disponibilidad");
            Console.WriteLine("4. Imprimir Listado");
            Console.WriteLine("5. Salir");
            Console.Write("Opcion: ");
        }

        static string PedirRestaurante()
        {
            Console.WriteLine("Restaurantes: Ember, Zao, Grappa, Larimar");
            Console.Write("Restaurante: ");
            string r = Console.ReadLine().Trim();
            if (!capacidades.ContainsKey(r)) { Console.WriteLine("Restaurante no valido."); return null; }
            return r;
        }

        static string PedirTurno()
        {
            Console.WriteLine("Turnos: A (6PM-8PM) | B (8PM-10PM)");
            Console.Write("Turno (A/B): ");
            string t = Console.ReadLine().Trim().ToUpper();
            if (t != "A" && t != "B") { Console.WriteLine("Turno no valido."); return null; }
            return "Turno" + t;
        }

        // Compara grupos actuales vs capacidad maxima
        static bool HayCupo(string restaurante, string turno) =>
            reservaciones[restaurante][turno].Count < capacidades[restaurante];

        static void RealizarReservacion()
        {
            Console.Write("\nNombre del cliente: ");
            string nombre = Console.ReadLine().Trim();
            nombre = char.ToUpper(nombre[0]) + nombre.Substring(1).ToLower();

            Console.Write("Cantidad de personas: ");
            int personas = int.Parse(Console.ReadLine());

            string restaurante = PedirRestaurante(); if (restaurante == null) return;
            string turno       = PedirTurno();       if (turno == null) return;

            if (!HayCupo(restaurante, turno))
            {
                Console.WriteLine($"Sin cupo en {restaurante} para ese turno."); return;
            }

            reservaciones[restaurante][turno].Add((nombre, personas));
            Console.WriteLine($"Reserva confirmada para {nombre} en {restaurante} ({turno}).");
        }

        static void EliminarReserva()
        {
            Console.Write("\nNombre del cliente a eliminar: ");
            string nombre = Console.ReadLine().Trim().ToLower();

            foreach (var restaurante in reservaciones.Keys)
            {
                foreach (var turno in reservaciones[restaurante].Keys)
                {
                    var lista  = reservaciones[restaurante][turno];
                    int indice = lista.FindIndex(r => r.nombre.ToLower() == nombre);

                    if (indice != -1)
                    {
                        lista.RemoveAt(indice);
                        Console.WriteLine($"Reserva eliminada de {restaurante} ({turno}).");
                        return;
                    }
                }
            }
            Console.WriteLine("No se encontro ninguna reserva con ese nombre.");
        }

        static void VerDisponibilidad()
        {
            string turno = PedirTurno(); if (turno == null) return;
            Console.WriteLine($"\n--- Disponibilidad {(turno == "TurnoA" ? "6PM-8PM" : "8PM-10PM")} ---");

            foreach (var r in capacidades.Keys)
            {
                int ocupados    = reservaciones[r][turno].Count;
                int disponibles = capacidades[r] - ocupados;
                string estado   = disponibles == 0 ? "FULL" : $"{disponibles} disponible(s)";
                Console.WriteLine($"{r,-10} {ocupados}/{capacidades[r]} grupos | {estado}");
            }
        }

        static void ImprimirListado()
        {
            string restaurante = PedirRestaurante(); if (restaurante == null) return;
            string turno       = PedirTurno();       if (turno == null) return;

            var lista = reservaciones[restaurante][turno];
            Console.WriteLine($"\n--- {restaurante} | {(turno == "TurnoA" ? "6PM-8PM" : "8PM-10PM")} ---");

            if (lista.Count == 0) { Console.WriteLine("No hay reservaciones registradas."); return; }

            int totalPersonas = 0;
            for (int i = 0; i < lista.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {lista[i].nombre} - {lista[i].personas} persona(s)");
                totalPersonas += lista[i].personas;
            }
            Console.WriteLine($"Grupos: {lista.Count} | Total personas: {totalPersonas}");
        }
    }
}