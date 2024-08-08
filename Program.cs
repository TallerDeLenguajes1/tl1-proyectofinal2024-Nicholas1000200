﻿using System;
using System.Collections.Generic;
using GeneradorAleatorioPjs;
using TextoAnimacion;
using Api;

class Program
{
    static async Task Main()
    {
        do
        {
            Texto.LogoSw();
            Texto.Menu();
            int opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    await EmpiezaJuego();
                    break;
                case 2:
                    VerGanadores();
                    break;
                case 3:
                    return;
                default:
                    Console.WriteLine("OPCION NO VALIDA\nLimitese a escribir un numero entre 1 y 3");
                    Console.ReadLine();
                    break;
            }
        } while (true);
    }
    static async Task EmpiezaJuego()
    {
        Player.Limpiar();
        string nombreArchivoPjs = "personajesJson.json";
        List<Player> personajes;

        if (PersonajesJson.PersonajesJson.Existe(nombreArchivoPjs))
        {
            personajes = PersonajesJson.PersonajesJson.LeerJson(nombreArchivoPjs);
        }
        else
        {
            personajes = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                personajes.Add(Player.PlayerGenerator());
            }
            PersonajesJson.PersonajesJson.GuardarPJJson(personajes, nombreArchivoPjs);
        }
        Console.WriteLine("\nElija su personaje:");
        for (int i = 0; i < personajes.Count; i++)
        {
            Console.WriteLine($"{i + 1} - Nombre:{personajes[i].Nombre} - Tipo:{personajes[i].Tipo}");
            Console.WriteLine($"Armadura:{personajes[i].Armadura}");
            Console.WriteLine($"Velocidad:{personajes[i].Velocidad}");
            Console.WriteLine($"Destreza:{personajes[i].Destreza}");
            Console.WriteLine($"Fuerza:{personajes[i].Fuerza}");
        }
        Console.WriteLine("Eleccion(1-10):");
        int eleccion = int.Parse(Console.ReadLine()) - 1;
        Player elegido = null;
        elegido = personajes[eleccion];
        //Obtenemos datos personales de el personaje elegido a traves de la api
        ApiSw.Character character;
         character = await ApiSw.ApiStarWars(elegido.Nombre);

        if (character != null)
        {
            Console.WriteLine($"Personaje encontrado: {character.Name}");
            Console.WriteLine($"Altura: {character.Height}");
            Console.WriteLine($"Peso: {character.Mass}");
            Console.WriteLine($"Color de pelo: {character.HairColor}");
            Console.WriteLine($"Color de piel: {character.SkinColor}");
        }
        else
        {
            Console.WriteLine("Personaje no encontrado.");
        }
        while (personajes.Count > 1)
        {
            Console.WriteLine("El combate comenzara en breve...\n");
            List<Player> ListaOponentes = new List<Player>(personajes);
            /* Borramos el personaje elegido previamente y asignamos un oponente aleatoreamente */
            ListaOponentes.Remove(elegido);
            Random rand = new Random();
            int indiceOponente = rand.Next(ListaOponentes.Count);
            Player oponente = ListaOponentes[indiceOponente];

            Console.WriteLine($"El epico combate entre {elegido.Nombre} y {oponente.Nombre} ha comenzado!!!\n");

            Gameplay.Gameplay.Batalla(elegido, oponente, personajes);

            if (elegido.Salud <= 0)
            {
                elegido = null;
                personajes.Clear();
                PersonajesJson.PersonajesJson.GuardarPJJson(personajes, nombreArchivoPjs);
                break;
            }
            else if (oponente.Salud <= 0)
            {
                personajes.Remove(oponente);
                if (personajes.Count > 1)
                {
                    Console.WriteLine("-RONDA GANADA- \n Pasaremos al siguiente combate a muerte");
                }
            }
            if (personajes.Count == 1)
            {
                Console.WriteLine("Derrotaste a todos tus enemigos, seras recordado como una leyenda hasta el fin de los tiempos");
                string archivoHistorial = "HistorialJson.json";
                HistorialJson.HistorialJson.GuardarJsonGanador(elegido, archivoHistorial);
                personajes.Clear();
            }
            PersonajesJson.PersonajesJson.GuardarPJJson(personajes,nombreArchivoPjs);
        }
    }
    static void VerGanadores()
    {
        Console.Clear();
        // Define la ruta del archivo JSON de ganadores
        string nombreArchivoG = "HistorialJson.json";

        // Verifica si el archivo de ganadores existe

        if (HistorialJson.HistorialJson.Existe(nombreArchivoG))
        {
            // Lee los ganadores del archivo JSON si este existe
            List<Player> ganadores = HistorialJson.HistorialJson.LeerJsonGanadores(nombreArchivoG);

            // Muestra la lista de ganadores
            Console.WriteLine("\nHistorial de ganadores:");
            foreach (var ganador in ganadores)
            {
                Console.WriteLine($"{ganador.Nombre}");
            }
        }
        else
        {
            // Muestra un mensaje si no hay historial de ganadores
            Console.WriteLine("No hay historial de ganadores.");
        }
    }
}


