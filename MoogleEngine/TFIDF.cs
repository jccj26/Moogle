

public class TFIDF
{
    // Cargar los documentos tal que la funcion devuelva un objeto del tipo empleado en el modelo vectorial


    public string Nombre { get; private set;} // nombre del txt
    public string Ruta { get; private set;} // ruta del txt
    float Cantidad_Palabras { get; set; } // cantidad de palabras del txt
    public Dictionary<string, float> tfd { get; set;} // diccionario que asocia la palabra con su frecuencia en el txt
    public string Texto {get; private set;}// el texto del documento

    public TFIDF(string ruta)
    {
        this.Ruta = ruta; // inicilizar la ruta
        this.tfd = new Dictionary<string, float>(); // inicializar el diccionario
        int apararicion_operador = ruta.LastIndexOf(Path.DirectorySeparatorChar); // ultima aparicion del \\
        Nombre = ruta.Substring(apararicion_operador +1); 
        StreamReader lector = new StreamReader(ruta); //lector de txt
        string texto = lector.ReadToEnd().ToLower();
        this.Texto = texto;
        lector.Close();

        char[] signos = { ' ', ',', ';', '.', ':', '\n', '\t'}; // signos que hay que quitar para normalizar el txt
        string[] palabras = texto.Split(signos, StringSplitOptions.RemoveEmptyEntries); // arreglo que contiene cada palabra del texto

        // por cada palabra en el arreglo que contiene todas las palabras de un documento voy a ver si esta ya en el diccionario, si esta le sumo 1 sino le pongo 1
        foreach (var palabra in palabras)
        {
            if(tfd.Keys.Contains(palabra))
            {
                tfd[palabra] ++;
            }
            else tfd[palabra] = 1;
        }
        this.Cantidad_Palabras = palabras.Length;
    }


    public float this[string palabra]
    {
        get
        {
            if(tfd.Keys.Contains(palabra))
            {
                return tfd[palabra] / Cantidad_Palabras;
            }
            else return 0f;
        }

    }

}