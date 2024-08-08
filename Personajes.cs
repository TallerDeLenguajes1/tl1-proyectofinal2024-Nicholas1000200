using System.Text.Json;
using GeneradorAleatorioPjs;
using System.IO;
using System.Collections.Generic;
namespace PersonajesJson
{
    class PersonajesJson
    {
        //Metodo para guardar los personajes en un json teniendo en cuenta si esta vacio o no
        public static void GuardarPJJson(List<Player> pjs, string nombreArchivo)
        {
            if (pjs.Count == 0)
            {
                string jsonEmpty = "";
                File.WriteAllText(nombreArchivo, jsonEmpty);
            }
            else
            {
                string json = JsonSerializer.Serialize(pjs);
                File.WriteAllText(nombreArchivo, json);

            }
        }
        //Metodo para leer los personajes de un json
        public static List<Player> LeerJson(string nombreArchivo)
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