using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using GeneradorAleatorioPjs;


namespace HistorialJson
{
    public class HistorialJson
    {
        //Metodo para guardar los ganadores
        public static void GuardarJsonGanador(Player pj, string nombreArchivo)
        {
            List<Player> Ganadores = LeerJsonGanadores(nombreArchivo);
            Ganadores.Add(pj);
            string json = JsonSerializer.Serialize(Ganadores);
            File.WriteAllText(nombreArchivo, json);
        }
        //Metodo para leer los ganadores
        public static List<Player> LeerJsonGanadores(string nombreArchivo)
        {
            if (File.Exists(nombreArchivo))
            {
                string json = File.ReadAllText(nombreArchivo);
                return JsonSerializer.Deserialize<List<Player>>(json);
            }
            return new List<Player>();
        }
        //Metodo para verificar el contenido de un JSON
        public static bool Existe(string nombreArchivo)
        {
            if (File.Exists(nombreArchivo))
            {
                string json = File.ReadAllText(nombreArchivo);
                return json.Length > 0;
            }
            return false;
        }

    }

}