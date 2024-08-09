using System.Text.Json;
using GeneradorAleatorioPjs;


namespace HistorialJson
{
    public class HistorialJson1
    {
        //Metodo para guardar los ganadores
        public void GuardarGanador(Player datoGanador, string nombreArchivo)
        {
            string ruta = nombreArchivo;
            var historial = new List<Player>();
            if (Existe(nombreArchivo))
            {
                historial = LeerJsonGanadores(nombreArchivo);
            }
            historial.Add(datoGanador);
            string historialString = CrearArchivoHistorialJson(historial);
            FileStream archivo = new FileStream(nombreArchivo, FileMode.OpenOrCreate);
            using (StreamWriter strwriter = new StreamWriter(archivo))
            {
                strwriter.WriteLine("{0}", historialString);
                strwriter.Close();
            }
        }
        public string CrearArchivoHistorialJson(List<Player> dato)
        {
            return JsonSerializer.Serialize(dato);
        }
        //Metodo para leer los ganadores
        public static List<Player> LeerJsonGanadores(string nombreArchivo)
        {
            if (File.Exists(nombreArchivo))
            {
                string json = File.ReadAllText(nombreArchivo);
                return JsonSerializer.Deserialize<List<Player>>(json);

            }else{
                Console.WriteLine("No hay lista de ganadores");
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