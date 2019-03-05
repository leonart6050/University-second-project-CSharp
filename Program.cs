using System;
/*Jorge Leonardo Trujillo Salas
  TRUJ12059003
  Numero B*/
class Personne
{
    private string naissance; // format "jj/mm/aaaa", ex : "25/12/1993"
    private char sexe;
    private int nbCafe;
    //Constructeur 3 parametres
    public Personne(char sexe, string naissance, int nbCafe)
    {
        this.sexe = sexe;
        this.naissance = naissance;
        this.nbCafe = nbCafe;
    }
    //Constructeur 2 parametres
    public Personne(char sexe, string naissance)
        :this(sexe,naissance,0) // appel au constructeur ayant 3 paramètres
    {
    }

    public char  Sexe
    {
        get { return sexe; }
    } 

    public int NbCafe
    {
        get { return nbCafe; }
        set
        {
            nbCafe = value > 0 ? value : 0;
        }
    }
    public void Afficher (string message)
    {
        string msg = message;        
        msg += this.sexe == 'F' ?  ":\nFemme née le " :  ":\nHomme né le ";
        msg += this.naissance.Substring(0,2) + " " + Mois + " " + this.naissance.Substring(6);
        msg += ", consomme " + this.nbCafe + " tasse(s) de café";
        Console.WriteLine("{0}",msg);
    }

    public override string ToString()
    {
        string pluriel = nbCafe > 1 ? "s" : string.Empty;
        return string.Format("'{0}'    \"{1}\"    {2} tasse" + pluriel ,sexe,naissance,nbCafe);
    }

    public string Mois
    {
        get
        {
            int mois = int.Parse(naissance.Substring(3, 2)) % 100;
            string[] nomMois = {"janvier", "février", "mars", "avril", "mai", "juin",
                           "juillet", "août", "septembre", "octobre",
                           "novembre", "décembre"};
            return nomMois[mois - 1];
        }
    }
} // fin de la classe Personne

class NumeroB
{
    static void detMax(Personne[] tab, int borne) //determiner maximum
    {
        int max = tab[0].NbCafe, indice = 0;
        for (int i = 0; i < borne; i++)
        {
            if (tab[i].Sexe == 'F' && tab[i].NbCafe > max )
            {
                max = tab[i].NbCafe;
                indice = i;
            }
        }
        Console.WriteLine("Informations de la femme qui consomme le plus de café\n" + tab[indice]);
    }

    static void reduirCafe (Personne[] tab, int borne)
    {
        foreach (Personne pers in tab)
            pers.NbCafe = pers.NbCafe -1 ;
    }

    static void afficher (Personne[] tab, int borne)
    {
        Console.WriteLine("Indice   Tableau pers\n-----------------------------------------------");
        for (int i = 0; i < tab.Length; i++)
            Console.WriteLine("{0,4})   {1}", i + 1, tab[i]);
    }

    static void trier (Personne[] tab , int borne) //trier à partir de Nombre de café
    {
        for (int i = 0; i < borne - 1; i++)
        {
            int indMin = i;
            for (int j = i + 1; j < borne; j++)
                if (tab[j].NbCafe < tab[indMin].NbCafe)
                    indMin = j;
            if (indMin != i) // on fait la permutation
            {
                Permuter(ref tab[i], ref tab[indMin]);
            }
        }
    }

    static void Permuter(ref Personne a, ref Personne b)
    {
        Personne tempo = a;
        a = b;
        b = tempo;
    }
    //Fonction pour compter persons nées dans un mois spécifique
    static void compterPerson (Personne[] tab, string mois, int borne)
    {
        int compteur = 0;
        foreach (Personne pers in tab)
            if (pers.Mois == mois) compteur++;
        Console.WriteLine("Il y a {0} personnes qui sont nées au mois de " + mois, compteur);
    }

    static void Main(string[] args)
    {
        Personne pers1 = new Personne('F', "19/02/1996", 3),
                 pers2 = new Personne('M', "27/07/1990");// par défaut 0 tasse
        pers1.Afficher("Informations de pers1");
        pers2.Afficher("Informations de pers2");
        Personne[] pers = new Personne[] {
        new Personne ('F', "16/11/1992", 2),
        new Personne ('F', "02/05/1990", 1),
        new Personne ('M', "23/11/1990", 5),
        new Personne ('F', "19/02/1985"),
        new Personne ('F', "30/11/1991", 3),
        new Personne ('M', "14/05/1997", 1),
        };
        
        afficher(pers, pers.Length);
        detMax(pers, pers.Length);
        reduirCafe(pers, pers.Length);
        afficher(pers, pers.Length);
        trier(pers, pers.Length);
        afficher(pers, pers.Length);
        compterPerson(pers, "novembre", pers.Length);
        Console.ReadLine();
    }
}
/*Execution:
Informations de pers1:
Femme née le 19 février 1996, consomme 3 tasse(s) de café
Informations de pers2:
Homme né le 27 juillet 1990, consomme 0 tasse(s) de café
Indice   Tableau pers
-----------------------------------------------
   1)   'F'    "16/11/1992"    2 tasses
   2)   'F'    "02/05/1990"    1 tasse
   3)   'M'    "23/11/1990"    5 tasses
   4)   'F'    "19/02/1985"    0 tasse
   5)   'F'    "30/11/1991"    3 tasses
   6)   'M'    "14/05/1997"    1 tasse
Informations de la femme qui consomme le plus de café
'F'    "30/11/1991"    3 tasses
Indice   Tableau pers
-----------------------------------------------
   1)   'F'    "16/11/1992"    1 tasse
   2)   'F'    "02/05/1990"    0 tasse
   3)   'M'    "23/11/1990"    4 tasses
   4)   'F'    "19/02/1985"    0 tasse
   5)   'F'    "30/11/1991"    2 tasses
   6)   'M'    "14/05/1997"    0 tasse
Indice   Tableau pers
-----------------------------------------------
   1)   'F'    "02/05/1990"    0 tasse
   2)   'F'    "19/02/1985"    0 tasse
   3)   'M'    "14/05/1997"    0 tasse
   4)   'F'    "16/11/1992"    1 tasse
   5)   'F'    "30/11/1991"    2 tasses
   6)   'M'    "23/11/1990"    4 tasses
Il y a 3 personnes qui sont nées au mois de novembre

 */
