using System;
using System.IO;
/*Jorge Leonardo Trujillo Salas
  TRUJ12059003
  Numero C*/
class Personne
{
    private string nom;
    private char sexe;
    private double taille, poids;
    private int numero;
    //Constructeur
    public Personne(string nom , char sexe, double taille, double poids, int numero)
    {
        this.nom = nom;
        this.sexe = sexe;
        this.taille = taille;
        this.poids = poids;
        this.numero = numero;
    }

    public char Sexe
    {
        get { return sexe; }
        set
        {
            sexe = value == 'F'? value : 'M';
            // ou simplement sexe = value;
        }
    }

    public int Numero
    {
        get { return numero; }
    }

    public string Nom
    {
        get { return nom; }
    }

    public double Taille
    {
        get { return taille; }
        set
        {
            taille = value;
        }
    }

    public override string ToString()
    {
        return string.Format("{0,-20}{1,6}{2,10:0.00}{3,8:0.0}{4,9}" , nom, sexe, taille, poids, numero);
    }

    public override bool Equals(object obj)
    {
        if (this == obj)
            return true;
        else if (this.GetType() != obj.GetType())
            return false;
        else
        {
            Personne autre = (Personne)obj;
            return Nom == autre.Nom;
        }
    }
} // fin de la classe Personne

class NumeroC
{
    static void Relire(string nomALire, ref Personne[] tab, out int nbPers)
    {
        nbPers = 0;
        StreamReader aLire = File.OpenText(nomALire);
        string ligneLue; // on lit ligne par ligne
        while ((ligneLue = aLire.ReadLine()) != null)
        {
            string nom = ligneLue.Substring(0, 20).Trim();
            char sexe = ligneLue[30];
            double taille  = Double.Parse(ligneLue.Substring(37, 4).Replace(",","."));
            double poids = Double.Parse((ligneLue.Substring(51, 5).Trim()).Replace(",", "."));
            int numero = Int32.Parse(ligneLue.Substring(64, 4));
            Personne temp = new Personne (nom, sexe, taille, poids,numero);
            tab[nbPers] = temp;
            nbPers++;
        }
        aLire.Close();
    }

    static void Affiche (Personne[] tab, int borne)
    {
        Console.WriteLine("\tNom\t\tSexe\tTaille\tPoids\tNumero\n------------------------------------------------------");
        foreach (Personne pers in tab)
            Console.WriteLine(pers);
    }
    //Modifier le sexe selon le numero
    static void Modifier (Personne[] tab, char sexeAChanger, int num, int borne)
    {
        foreach (Personne per in tab)
            if (per.Numero == num)
                per.Sexe = sexeAChanger;
    }
    //Recherche sequentielle
    static void rechercher (Personne[] tab, string nom, int borne)
    {
        bool trouve = false;
        Personne temp = new Personne(nom.ToUpper(), ' ', 0.0, 0.0, 0);
        foreach (Personne per in tab) //parcourrir chaqu'une des personnes
        {
            if (per.Equals(temp))
            {
                trouve = true;//si trouve on transforme en true
                Console.WriteLine(nom + " est trouvée:\n" + per + "\n");
            }
        }
        if (!trouve)//si rien trouve on affiche personne introuvable
            Console.WriteLine(nom + " est introuvable");        
    }

    static void QuickSort(Personne[] tab, int gauche, int droite)
    {
        int indPivot;
        if (droite > gauche) /* au moins 2 Ã©lÃ©ments */
        {
            Partitionner(tab, gauche, droite, out indPivot);
            QuickSort(tab, gauche, indPivot - 1);
            QuickSort(tab, indPivot + 1, droite);
        }
    }

    static void Partitionner(Personne[] tab, int debut, int fin, out int indPivot)
    {
        int g = debut, d = fin;
        int valPivot = tab[debut].Numero;
        do
        {
            while (g <= d && tab[g].Numero <= valPivot) g++;
            while (tab[d].Numero > valPivot) d--;

            if (g < d) Permuter(ref tab[g], ref tab[d]);

        } while (g <= d);
        Permuter(ref tab[debut], ref tab[d]);
        indPivot = d;
    }

    static void Permuter(ref Personne a, ref Personne b)
    {
        Personne tempo = a;
        a = b;
        b = tempo;
    }
    //Recherche Dichotomique
    static void RechercherDicho(Personne[] tab, int aChercher , int borne)
    {
            int k = indDicho(aChercher, tab, borne);
            if (k != -1) // trouvé
            {
                Console.WriteLine("On trouve {0} à l'indice {1}", aChercher, k + 1);
                Console.WriteLine("{0,3}) {1,8}", k + 1,tab[k]);
            }
            else
                Console.WriteLine("On ne trouve pas le numéro : {0} ", aChercher);
    }

    static int indDicho(int aChercher, Personne[] tab, int borne)
    {
        int mini = 0, maxi = borne - 1;
        while (mini <= maxi)
        {
            int milieu = (mini + maxi) / 2;
            if (aChercher < tab[milieu].Numero)
                maxi = milieu - 1;
            else
                if (aChercher > tab[milieu].Numero)
                mini = milieu + 1;
            else
                return milieu;
        }
        return -1;
    }

    static void ModifierTaille (Personne[] tab, int perso, double aAjouter, int borne)
    {
        int indice = indDicho(perso, tab, borne);
        tab[indice].Taille += aAjouter;
    }

    static void Supprimer (Personne[] tab, int perso, int borne)
    {
        int indice = indDicho(perso, tab, borne);
        for (int i = indice ; i< borne -1 ; i++)
            tab[i] = tab[i + 1];
        tab[borne-1] = null;
        Console.WriteLine("Personne {0} eliminée", perso);
    }
    static void Inserer (Personne[] tab, Personne aAjouter, int borne)
    {
        int indice = -1;
        //A cause que le tableau est trie, o cherche le lieu exacte pour l'inserer
        for (int i = 0; i < borne - 1; i++)
            if (tab[i].Numero < aAjouter.Numero && tab[i + 1].Numero > aAjouter.Numero)
                indice = i + 1;
        //on deplace les personnes commençant par la derniere
        for (int i = borne-1; i > indice ; i--)
            tab[i] = tab[i - 1];
        //et finalement on ajoute la nouvelle personne
        tab[indice] = aAjouter;
        Console.WriteLine("Personne {0} ajoutée", aAjouter.Nom);
    }

    static void Main(string[] args)
    {
        int nbLues = 0;
        Personne[] pers = new Personne[28];
        Relire(@"..\..\..\met_h17.txt", ref pers, out nbLues);
        Console.WriteLine("nombre de personnes lues: {0}", nbLues);
        Affiche(pers, nbLues);
        Modifier(pers,'F',2325, nbLues);
        rechercher(pers, "Coutu Pierre", nbLues);
        rechercher(pers, "Robitaille Suzanne", nbLues);
        rechercher(pers, "Gagnon Daniel", nbLues);
        QuickSort(pers, 0 , nbLues-1);
        Affiche(pers, nbLues);
        RechercherDicho(pers, 4612, nbLues);
        RechercherDicho(pers, 4371, nbLues);
        RechercherDicho(pers, 2325, nbLues);
        RechercherDicho(pers, 9876, nbLues);
        Supprimer(pers, 4371, nbLues);
        ModifierTaille(pers, 2636, 0.04, nbLues);
        Personne temp = new Personne("GOSSELIN LAURENT", 'M', 1.68, 72.1, 3430);
        Inserer(pers, temp, nbLues);
        Affiche(pers, nbLues);
        Console.ReadLine();
    }
}
/*Execution:
nombre de personnes lues: 28
        Nom             Sexe    Taille  Poids   Numero
------------------------------------------------------
ROY CHANTAL              F      1.75    57.9     2754
MOLAISON CLAUDE          M      1.57    62.2     1848
ROBITAILLE CHANTALE      F      1.79    72.3     2007
BEDARD MARC-ANDRE        M      1.43    55.5     2636
MONAST STEPHANE          M      1.65    61.7     1750
JALBERT LYNE             F      1.63    52.6     2168
DUBE FRANCOISE           F      1.68    67.5     4612
ROBITAILLE SUZANNE       M      1.72    75.4     2325
LEMELIN SOPHIE           F      1.88    57.8     7777
LABELLE LISE             F      1.79    68.0     1512
CHOQUETTE HELENE         F      1.71    60.8     2340
ROBITAILLE SUZANNE       F      1.82    76.1     8007
MICHAUD NORMAND          M      1.73   103.7     3428
RICHER AGATHE            F      1.65    53.1     3563
ROY SERGE                M      1.70    74.3     1488
BEGIN MARIE-LUCE         F      1.62    49.0     4101
ROBITAILLE SUZANNE       F      1.63    75.1     7654
COUTU PIERRE             M      1.72    62.1     4008
TREMBLAY SUZANNE         F      1.48    61.5     4371
BERGEVIN GUILLAUME       M      1.84    86.4     2277
DUMITRU PIERRE           M      1.82    99.4     3629
ROBITAILLE MICHEL        M      1.78    85.1     6002
GOFFIN SYLVIE            F      1.65    53.1     5505
FILLION ERIC             M      1.78    75.7     2630
DESMARAIS DENISE         F      1.79    58.7     3215
TREMBLAY MARC            M      1.79    64.9     3529
TREMBLAY SYLVAIN         M      1.83    86.2     1538
ROBITAILLE SUZANNE       F      1.68    60.2     4119

Coutu Pierre est trouvée:
COUTU PIERRE             M      1.72    62.1     4008

Robitaille Suzanne est trouvée:
ROBITAILLE SUZANNE       F      1.72    75.4     2325

Robitaille Suzanne est trouvée:
ROBITAILLE SUZANNE       F      1.82    76.1     8007

Robitaille Suzanne est trouvée:
ROBITAILLE SUZANNE       F      1.63    75.1     7654

Robitaille Suzanne est trouvée:
ROBITAILLE SUZANNE       F      1.68    60.2     4119

Gagnon Daniel est introuvable

        Nom             Sexe    Taille  Poids   Numero
------------------------------------------------------
ROY SERGE                M      1.70    74.3     1488
LABELLE LISE             F      1.79    68.0     1512
TREMBLAY SYLVAIN         M      1.83    86.2     1538
MONAST STEPHANE          M      1.65    61.7     1750
MOLAISON CLAUDE          M      1.57    62.2     1848
ROBITAILLE CHANTALE      F      1.79    72.3     2007
JALBERT LYNE             F      1.63    52.6     2168
BERGEVIN GUILLAUME       M      1.84    86.4     2277
ROBITAILLE SUZANNE       F      1.72    75.4     2325
CHOQUETTE HELENE         F      1.71    60.8     2340
FILLION ERIC             M      1.78    75.7     2630
BEDARD MARC-ANDRE        M      1.43    55.5     2636
ROY CHANTAL              F      1.75    57.9     2754
DESMARAIS DENISE         F      1.79    58.7     3215
MICHAUD NORMAND          M      1.73   103.7     3428
TREMBLAY MARC            M      1.79    64.9     3529
RICHER AGATHE            F      1.65    53.1     3563
DUMITRU PIERRE           M      1.82    99.4     3629
COUTU PIERRE             M      1.72    62.1     4008
BEGIN MARIE-LUCE         F      1.62    49.0     4101
ROBITAILLE SUZANNE       F      1.68    60.2     4119
TREMBLAY SUZANNE         F      1.48    61.5     4371
DUBE FRANCOISE           F      1.68    67.5     4612
GOFFIN SYLVIE            F      1.65    53.1     5505
ROBITAILLE MICHEL        M      1.78    85.1     6002
ROBITAILLE SUZANNE       F      1.63    75.1     7654
LEMELIN SOPHIE           F      1.88    57.8     7777
ROBITAILLE SUZANNE       F      1.82    76.1     8007
On trouve 4612 à l'indice 23
 23) DUBE FRANCOISE           F      1.68    67.5     4612
On trouve 4371 à l'indice 22
 22) TREMBLAY SUZANNE         F      1.48    61.5     4371
On trouve 2325 à l'indice 9
  9) ROBITAILLE SUZANNE       F      1.72    75.4     2325
On ne trouve pas le numéro : 9876
Personne 4371 eliminée
Personne GOSSELIN LAURENT ajoutée
        Nom             Sexe    Taille  Poids   Numero
------------------------------------------------------
ROY SERGE                M      1.70    74.3     1488
LABELLE LISE             F      1.79    68.0     1512
TREMBLAY SYLVAIN         M      1.83    86.2     1538
MONAST STEPHANE          M      1.65    61.7     1750
MOLAISON CLAUDE          M      1.57    62.2     1848
ROBITAILLE CHANTALE      F      1.79    72.3     2007
JALBERT LYNE             F      1.63    52.6     2168
BERGEVIN GUILLAUME       M      1.84    86.4     2277
ROBITAILLE SUZANNE       F      1.72    75.4     2325
CHOQUETTE HELENE         F      1.71    60.8     2340
FILLION ERIC             M      1.78    75.7     2630
BEDARD MARC-ANDRE        M      1.47    55.5     2636
ROY CHANTAL              F      1.75    57.9     2754
DESMARAIS DENISE         F      1.79    58.7     3215
MICHAUD NORMAND          M      1.73   103.7     3428
GOSSELIN LAURENT         M      1.68    72.1     3430
TREMBLAY MARC            M      1.79    64.9     3529
RICHER AGATHE            F      1.65    53.1     3563
DUMITRU PIERRE           M      1.82    99.4     3629
COUTU PIERRE             M      1.72    62.1     4008
BEGIN MARIE-LUCE         F      1.62    49.0     4101
ROBITAILLE SUZANNE       F      1.68    60.2     4119
DUBE FRANCOISE           F      1.68    67.5     4612
GOFFIN SYLVIE            F      1.65    53.1     5505
ROBITAILLE MICHEL        M      1.78    85.1     6002
ROBITAILLE SUZANNE       F      1.63    75.1     7654
LEMELIN SOPHIE           F      1.88    57.8     7777
ROBITAILLE SUZANNE       F      1.82    76.1     8007 
*/