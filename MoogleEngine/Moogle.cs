namespace MoogleEngine;

public static class Moogle
{

    public static SearchResult Query(string query) {
        
        
        //Cargar solo 1 vez
        if(!cargado){
            cargado = true;
            string[] rutas = Directory.EnumerateFiles(content).ToArray();
            
            //Tantos documentos como rutas a archivos
            archivos = new TFIDF[rutas.Length];

            for(int i=0;i<archivos.Length;i++){
                archivos[i] = new TFIDF(rutas[i]);
            }
        }

        string[] palabrasQuery = query.Split(' ');

        SearchItem[] items = new SearchItem[archivos.Length];
        for(int i=0;i<items.Length;i++){
            items[i] = new SearchItem(archivos[i].Nombre,Snippet(archivos[i],palabrasQuery),Score(archivos[i],palabrasQuery));
        }

        items = Ordenar(items);
        //
        List<SearchItem> arr = new List<SearchItem>();
        foreach (var cosa in items) {
            if (cosa.Score == 0) continue;
            arr.Add(cosa);
        }

        SearchItem[] answer = new SearchItem[arr.Count()];
        for (int i = 0; i < arr.Count(); i++) {
            answer[i] = arr[i];
        }
    
        return new SearchResult(answer,"");
    }


    public static Boolean cargado = false;
    public static TFIDF[] archivos;

    //La capeta content
    private static string content = Path.GetFullPath(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "Content");

    //Calcula el score de un documento
    private static float Score(TFIDF doc,string[] query){
        float score = 0;
        foreach(string palabra in query){
            score += doc[palabra] * Idf(palabra);
        }
        return score;
    }
    //Calcula el idf de un documento
    private static float Idf(string palabra){
        //cantidad de documentos donde aparece la palabra
        float cntApariciones = 0;
        foreach(var doc in archivos){
            if(doc[palabra] != 0)++cntApariciones;
        }
        
        float idf = (float) Math.Log2( (1 + archivos.Length) / (1 + cntApariciones));
        return idf;
    }

    private static string Snippet(TFIDF doc,string[] query){
        string snippet = "";
        foreach(string palabra in query){
            //Si no esta en el documento
            if(doc[palabra] == 0)continue;
            
            //Agrega al snippet las siguientes 100 caracteres
            string texto = doc.Texto;
            for(int index = texto.IndexOf(palabra), pasos = 100;index < texto.Length && pasos > 0;index++,pasos--){
                snippet += texto[index];
            }
            return snippet;
        }
        return snippet;
    }

    //Ordena de mayor a menor segun el score
    private static SearchItem[] Ordenar(SearchItem[] items){
        SearchItem[] newItems = new SearchItem[items.Length];
        for(int i=0;i<newItems.Length;i++){
            int index = i;//Indice del de mayor score de los que quedan
            for(int j=i + 1;j<newItems.Length;j++){
                if(items[j].Score > items[index].Score){
                    index = j;
                }
            }
            if(index != i){
                SearchItem aux = new SearchItem(items[i]);
                items[i] = new SearchItem(items[index]);
                items[index] = new SearchItem(aux);
            }
            newItems[i] = items[i];
        }
        return newItems;
    }
}    