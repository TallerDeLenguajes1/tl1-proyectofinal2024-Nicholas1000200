﻿using System;
using System.Collections.Generic;
using GeneradorAleatorioPjs;
using TextoAnimacion;
using Api;
using HistorialJson;

class Program
{
    static async Task Main()
    {
        do
        {
            Texto.LogoSw();
            Texto.Menu();
            string opcion = Console.ReadLine().ToUpper();
            switch (opcion)
            {
                case "1":
                    await EmpiezaJuego();
                    break;
                case "2":
                    VerGanadores();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("OPCION NO VALIDA\nLimitese a escribir un numero entre 1 y 3");
                    Console.WriteLine("Volver al menu...");
                    Console.ReadLine();
                    break;
            }
        } while (true);
    }
    static async Task EmpiezaJuego()
    {
        Player.Limpiar();
        string nombreArchivoPjs = "personajes/personajesJson.json";
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
            Texto.Animacion(15, $"{i + 1} - Nombre: {personajes[i].Nombre} - Tipo: {personajes[i].Tipo}\n");
            Texto.Animacion(15, $"Armadura: {personajes[i].Armadura}\n");
            Texto.Animacion(15, $"Velocidad: {personajes[i].Velocidad}\n");
            Texto.Animacion(15, $"Destreza: {personajes[i].Destreza}\n");
            Texto.Animacion(15, $"Fuerza: {personajes[i].Fuerza}\n");
            Texto.Animacion(15, $"Nivel: {personajes[i].Nivel}\n");
        }

        Console.WriteLine("Eleccion(1-10):");
        int Seleccion;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(),out Seleccion) && Seleccion >= 1 && Seleccion <= personajes.Count)
            {
                break;
            }
            Console.WriteLine("Elija un numero entre 1 y 10 por favor");
        }
        Player elegido = personajes[Seleccion - 1];
        //Obtenemos datos personales de el personaje elegido a traves de la api
        ApiSw.Character character;
        character = await ApiSw.ApiStarWars(elegido.Nombre);

        if (character != null)
        {
            Texto.Animacion(15, $"Caracteristicas de el personaje {character.Name} elegido\n");
            Texto.Animacion(15, $"Altura: {character.Height}\n");
            Texto.Animacion(15, $"Fecha de Nacimiento: {character.BirthDate}\n");
            Texto.Animacion(15, $"Color de piel: {character.SkinColor}\n");
            Texto.Animacion(15, $"Genero: {character.Gender}\n");
        }
        else
        {
            Console.WriteLine("Personaje no encontrado.");
        }
        while (personajes.Count > 1)
        {
            Console.WriteLine("El combate comenzara en breve...\n");
            Console.WriteLine("Presione Enter para comenzar");
            Console.ReadLine();
            List<Player> ListaOponentes = new List<Player>(personajes);
            /* Borramos el personaje elegido previamente y asignamos un oponente aleatoreamente */
            ListaOponentes.Remove(elegido);
            Random rand = new Random();
            int indiceOponente = rand.Next(ListaOponentes.Count);
            Player oponente = ListaOponentes[indiceOponente];

            Texto.Animacion(20,$"El epico combate entre {elegido.Nombre} y {oponente.Nombre} ha comenzado!!!\n");

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
                    Console.WriteLine("-RONDA GANADA-\n Pasaremos al siguiente combate a muerte\n");
                }
            }
            if (personajes.Count == 1)
            {
                Console.WriteLine("Derrotaste a todos tus enemigos, seras recordado como una leyenda hasta el fin de los tiempos");
                string archivoHistorial = "historial/HistorialJson.json";
                var historia = new HistorialJson1();
                /* HistorialJson.HistorialJson.(elegido, archivoHistorial) */;
                historia.GuardarGanador(elegido,archivoHistorial);
                personajes.Clear();
            }
            PersonajesJson.PersonajesJson.GuardarPJJson(personajes, nombreArchivoPjs);
        }
    }
    static void VerGanadores()
    {
        Console.Clear();
        // Define la ruta del archivo JSON de ganadores
        string nombreArchivoG = "historial/HistorialJson.json";
        bool existe = HistorialJson.HistorialJson1.Existe(nombreArchivoG);

        // Verifica si el archivo de ganadores existe

        if (existe)
        {
            // Lee los ganadores del archivo JSON si este existe
            List<Player> ganadores = HistorialJson.HistorialJson1.LeerJsonGanadores(nombreArchivoG);

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
            Texto.Animacion(15,"No hay historial de ganadores.");
        }
        Console.WriteLine("Volver al menu...");
        Console.ReadLine();
    }
}


