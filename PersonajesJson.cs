using System.Text.Json;
using GeneradorAleatorioPjs;
using System.IO;
using System.Collections.Generic;
namespace PersonajesJson
{
    class PersonajesJson
    {
        //Metodo para guardar los personajes en un json
        public static void GuardarJson(List<Player> pjs, string nombreArchivo)
        {
            string json = JsonSerializer.Serialize(pjs);
            File.WriteAllText(nombreArchivo, json);
        }
        //Metodo para leer los personajes de un json
        public static List<Player>LeerJson(string nombreArchivo){
            if(File.Exists(nombreArchivo)){
                string json = File.ReadAllText(nombreArchivo);
                return JsonSerializer.Deserialize<List<Player>>(json);
            }
            return new List<Player>();
        }
        //Metodo para verificar el contenido de un JSON
        public static bool Existe(string nombreArchivo){
            if (File.Exists(nombreArchivo))
            {
                string json = File.ReadAllText(nombreArchivo);
                return json.Length > 0;
            }
            return false;
        }
    }
}