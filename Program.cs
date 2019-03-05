using System;
/*Jorge Leonardo Trujillo Salas
  TRUJ12059003
  Numero A*/
class NumeroA
{
    static void afficher(string telephone, string message)
    {
        Console.WriteLine(message + "(" + telephone.Substring(0, 3) + ") " + telephone.Substring(3,3) + "-" + telephone.Substring(6));
    }

    static int compter (string telephone , char aChercher)
    {
        int n = 0;
        foreach (int element in telephone)
        {
            if (element == aChercher)
                n++;    
        }
        return n;
    }
    //Retourner le nombre de chiffres pairs ou impairs contenus dans un numero de
    //téléphone, dans le string appelé lesChiffres on sauvgarde les chiffres trouvées
    static int pairImpair (string telephone, string type, out string lesChiffres)
    {
        int n = 0;
        lesChiffres = "";
        foreach (char element in telephone)
        {
            switch (type)
            {
                case "pair":
                    if (estPair(element))
                    {
                        n++;
                        lesChiffres += " " + element;                        
                    }
                    break;
                case "impair":
                    if (!estPair(element))
                    {
                        n++;
                        lesChiffres += " " + element;
                    }
                    break;
                default:
                    break;// ce "dernier" break est OBLIGATOIRE en C# (il est facultatif en Java)
            }            
        }
        return n;
    }
    //Détermine si la chiffre est pair ou non
    static bool estPair(int chiffre)
    {
        return (chiffre % 2 == 0) ? true : false;
    }
    //Trouver les chifres communs entre les deux téléphones
    static void communs(string premier, string deuxieme, out string lesChiffres)
    {
        lesChiffres = "";
        string nombres = "0123456789";  //nombres en ordre croissant, utilisés pour comparer
        foreach (char chiffre in nombres)
        {
            if (premier.Contains(chiffre.ToString()) && deuxieme.Contains(chiffre.ToString()))
                lesChiffres += " " + chiffre;           
        }
    }
    //Trouver le max
    static char Maximum (string telephone)
    {
        char max = telephone[0];
        foreach (char chiffre in telephone)
        {
            if (chiffre > max)
                max = chiffre;
        }
        return max;
    }

    static void Main(string[] args)
    {
        string telUDM = "5143436111", telJean = "4501897654", chaineChiffre;
        int nbChiffres = telJean.Length;

        afficher(telUDM, "Téléphone d’UdM : ");
        afficher(telJean, "Téléphone de Jean : ");        
        int fois = compter(telUDM, '1');
        Console.WriteLine("Il y a {0} fois le chiffre 1 dans le numéro de téléphone d’UdM",fois);
        fois = compter(telJean, '2');
        Console.WriteLine("Il y a {0} fois le chiffre 2 dans le numéro de téléphone de Jean", fois);
        fois = compter(telJean, '4');
        Console.WriteLine("Il y a {0} fois le chiffre 4 dans le numéro de téléphone de Jean", fois);
        fois = pairImpair(telUDM, "pair", out chaineChiffre);
        Console.WriteLine("Il y a {0} fois les chiffres pairs dans le numéro de téléphone d’UdM,\nCe sont:{1}", fois, chaineChiffre);
        fois = pairImpair(telJean, "impair", out chaineChiffre);
        Console.WriteLine("Il y a {0} fois les chiffres impairs dans le numéro de téléphone de Jean,\nCe sont:{1}", fois, chaineChiffre);
        communs(telUDM, telJean, out chaineChiffre);
        Console.WriteLine("Les chiffres communs de ces 2 numéros de téléphone sont:{0}", chaineChiffre);
        char max = Maximum(telUDM);
        Console.WriteLine("Le chiffre {0} est le plus grand chiffre dans le numéro de téléphone d’UdM", max);
        max = Maximum(telJean);
        Console.WriteLine("Le chiffre {0} est le plus grand chiffre dans le numéro de téléphone de Jean", max);
        Console.ReadLine();
    }
}
/*Execution:
Téléphone d'UdM : (514) 343-6111
Téléphone de Jean : (450) 189-7654
Il y a 4 fois le chiffre 1 dans le numéro de téléphone d'UdM
Il y a 0 fois le chiffre 2 dans le numéro de téléphone de Jean
Il y a 2 fois le chiffre 4 dans le numéro de téléphone de Jean
Il y a 3 fois les chiffres pairs dans le numéro de téléphone d'UdM,
Ce sont: 4 4 6
Il y a 5 fois les chiffres impairs dans le numéro de téléphone de Jean,
Ce sont: 5 1 9 7 5
Les chiffres communs de ces 2 numéros de téléphone sont: 1 4 5 6
Le chiffre 6 est le plus grand chiffre dans le numéro de téléphone d'UdM
Le chiffre 9 est le plus grand chiffre dans le numéro de téléphone de Jean

 */
