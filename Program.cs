using System;

var listaArtikla = new List<(string Name, int Quant, float Price, DateOnly Date)>();
var listaArtikla2 = new List<(string Name, int Quant, float Price, DateOnly Date)>();
var listaZaPrviRacun = new List<(string Name, int Quant, float Price)>();
var listaZaRacune = new List<(string Name, int Quant, float Price)>();
var radnici = new Dictionary<string, DateOnly>()
{
    {"Petar Golem", new DateOnly(2001, 05, 20)}
};
listaZaPrviRacun.Add(("babane", 10, 4));
var racuni = new Dictionary<(int, DateTime), List<(string Name, int Quant, float Price)>>
{
    {(0, new DateTime()), listaZaPrviRacun}
};
listaArtikla.Add(("pome", 5, 2, new DateOnly(2023, 11, 20)));
var id = 0;
do
{
    Console.Clear();
    Console.WriteLine("1 - Artikli\n2 - Radnici\n3 - Računi\n4 - Statistika\n0 - Izlaz iz aplikacije");

    int prvi;
    int drugi;
    var treci = "0";
    do
    {
        if (int.TryParse(Console.ReadLine(), out prvi) && 5 > prvi && prvi >=0)
            break;
        Console.WriteLine("Upisi odgovarajucu vrijednost");
    } while (true);

    if (prvi == 1)
    {
        
        do
        {
            Console.Clear();
            Console.WriteLine("-- za povratak na glavni izbornik upisite \"0\" --");
            Console.WriteLine("1. Unos artikla (sve potrebne informacije)\n2. Brisanje artikla\n3. Uređivanje artikla\n4. Ispis");
            do
            {
                if (int.TryParse(Console.ReadLine(), out drugi) && 5 > prvi && prvi >= 0)
                    break;
                Console.WriteLine("Upisi odgovarajucu vrijednost");
            } while (true);
            if (drugi == 1)
                listaArtikla.Add(noviArtikl());
            else if (drugi == 2)
            {
                Console.Clear();
                Console.WriteLine("-- za povratak na glavni izbornik upisite \"0\" --");
                Console.WriteLine("a. Po imenu artikla\nb. Sve one kojima je istekao datum trajanja");
                do
                {
                    treci = Console.ReadLine();
                    switch (treci)
                    {
                        case "a":
                            Console.WriteLine("Koji artikl zelite izbrisati?");
                            var ime = Console.ReadLine();
                            foreach (var item in listaArtikla)
                            {
                                if (item.Name == ime)
                                {
                                    listaArtikla.Remove(item);
                                    break;
                                }
                            }
                            treci = "0";
                            break;
                        case "b":
                            for (int i = 0; i < listaArtikla.Count; i++)
                            {
                                foreach (var item in listaArtikla)
                                {
                                    if (item.Date < DateOnly.FromDateTime(DateTime.Now))
                                    {
                                        listaArtikla.Remove(item);
                                        break;
                                    }
                                }
                            }
                            treci = "0";
                            break;
                        default:
                            Console.WriteLine("Upisi tocnu vrijednost!");
                            break;
                    }
                } while (treci != "0");
            }
            else if (drugi == 3)
            {
                Console.Clear();
                Console.WriteLine("-- za povratak na glavni izbornik upisite \"0\" --");
                Console.WriteLine("a. Zasebno proizvoda\nb. Popopust/poskupljenje na sve proizvode unutar trgovine");
                do
                {
                    treci = Console.ReadLine();
                    switch (treci)
                    {
                        case "a":
                            Console.WriteLine("-- za povratak na glavni izbornik upisite \"0\" --");
                            do
                            {
                                Console.WriteLine("Koji artikl zelite urediti?");
                                var ime = Console.ReadLine();
                                if (ime == "0")
                                    break;
                                for (int i = 0; i < listaArtikla.Count; i++)
                                {
                                    if (listaArtikla[i].Name == ime)
                                    {
                                        listaArtikla2.Add(listaArtikla[i]);
                                        listaArtikla.Remove(listaArtikla[i]);
                                    }
                                }
                                if (!listaArtikla2.Any())
                                    Console.WriteLine("Nije pronaden artikl!");
                                else
                                {
                                    Console.WriteLine("Sto zelite urediti?\n\t1- kolicinu\n\t2- cijenu\n\t3- datum");
                                    var ured = 0;
                                    do
                                    {
                                        if (int.TryParse(Console.ReadLine(), out ured) && 4 > ured && ured > 0)
                                            break;
                                        Console.WriteLine("Upisi odgovarajucu vrijednost");
                                    } while (true);
                                    switch (ured)
                                    {
                                        case 1:
                                            Console.WriteLine("Unesi novu kolicinu: ");
                                            var kol = 0;
                                            do
                                            {
                                                if (int.TryParse(Console.ReadLine(), out kol))
                                                    break;
                                                Console.WriteLine("Upisi odgovarajucu vrijednost");
                                            } while (true);
                                            listaArtikla.Add((listaArtikla2[0].Name, kol, listaArtikla2[0].Price, listaArtikla2[0].Date));
                                            listaArtikla2.Clear();
                                            break;
                                        case 2:
                                            Console.WriteLine("Unesi novu cijenu: ");
                                            var cij = 0;
                                            do
                                            {
                                                if (int.TryParse(Console.ReadLine(), out cij))
                                                    break;
                                                Console.WriteLine("Upisi odgovarajucu vrijednost");
                                            } while (true);
                                            listaArtikla.Add((listaArtikla2[0].Name, listaArtikla2[0].Quant, cij, listaArtikla2[0].Date));
                                            listaArtikla2.Clear();
                                            break;
                                        case 3:
                                            var noviDat = datum();
                                            listaArtikla.Add((listaArtikla2[0].Name, listaArtikla2[0].Quant, listaArtikla2[0].Price, noviDat));
                                            listaArtikla2.Clear();
                                            break;

                                    }
                                }
                            } while (!listaArtikla2.Any());
                            treci = "0";
                            break;
                        case "b":
                            Console.WriteLine("Za koliko zelite poskupiti cijenu? (za popuste upisati negativnu vrijednost)");
                            float promjena;
                            do
                            {
                                if (float.TryParse(Console.ReadLine(), out promjena))
                                    break;
                                Console.WriteLine("Upisi odgovarajucu vrijednost");
                            } while (true);
                            for (int i = 0; i < listaArtikla.Count; i++)
                            {
                                Console.WriteLine(listaArtikla.Count);
                                listaArtikla2.Add((listaArtikla[i].Name, listaArtikla[i].Quant, listaArtikla[i].Price + promjena, listaArtikla[i].Date));
                            }
                            listaArtikla.Clear();
                            listaArtikla.AddRange(listaArtikla2);
                            listaArtikla2.Clear();
                            treci = "0";
                            break;
                        default:
                            Console.WriteLine("Upisi tocnu vrijednost!");
                            break;
                    }
                } while (treci != "0");
            }
            else if (drugi == 4)
            {
                Console.Clear();
                Console.WriteLine("-- za povratak na glavni izbornik upisite \"0\" --");
                Console.WriteLine("a. Svih artikala kako su spremljeni\nb. Svih artikala sortirano po imenu\n" +
                    "c. Svih artikala sortirano po datumu silazno\nd. Svih artikala sortirano po datumu uzlazno\n" +
                    "e. Svih artikala sortirano po količini");
                do
                {
                    treci = Console.ReadLine();
                    switch (treci)
                    {
                        case "a":
                            foreach (var item in listaArtikla)
                                Console.WriteLine($"{item}");
                            treci = "0";
                            break;
                        case "b":
                            listaArtikla2.AddRange(listaArtikla.OrderBy(x => x.Name).ToList());
                            foreach (var item in listaArtikla2)
                                Console.WriteLine($"{item}");
                            listaArtikla2.Clear();
                            treci = "0";
                            break;
                        case "c":
                            listaArtikla2.AddRange(listaArtikla.OrderBy(x => x.Date).ToList());
                            listaArtikla2.Reverse();
                            foreach (var item in listaArtikla2)
                                Console.WriteLine($"{item}");
                            listaArtikla2.Clear();
                            treci = "0";
                            break;
                        case "d":
                            listaArtikla2.AddRange(listaArtikla.OrderBy(x => x.Date).ToList());
                            foreach (var item in listaArtikla2)
                                Console.WriteLine($"{item}");
                            listaArtikla2.Clear();
                            treci = "0";
                            break;
                        case "e":
                            listaArtikla2.AddRange(listaArtikla.OrderBy(x => x.Quant).ToList());
                            foreach (var item in listaArtikla2)
                                Console.WriteLine($"{item}");
                            listaArtikla2.Clear();
                            treci = "0";
                            break;
                        default:
                            Console.WriteLine("Upisi tocnu vrijednost!");
                            break;
                    }
                } while (treci != "0");
                var izlaz = Console.ReadLine();
            }

        } while (drugi!=0);

    }







    else if(prvi==2)
    {
        do
        {
            Console.Clear();
            Console.WriteLine("-- za povratak na glavni izbornik upisite \"0\" --");
            Console.WriteLine("1. Unos radnika (sve potrebne informacije)\n2. Brisanje radnika\n3. Uređivanje radnika\n4. Ispis");
            do
            {
                if (int.TryParse(Console.ReadLine(), out drugi) && 5 > prvi && prvi >= 0)
                    break;
                Console.WriteLine("Upisi odgovarajucu vrijednost");
            } while (true);
            if (drugi == 1)
            {
                Console.WriteLine("Unesi ime i prezime: ");
                var ime=Console.ReadLine();
                Console.WriteLine("Unesi datum rodenja: ");
                var dat = datum();
                radnici.Add(ime, dat);
            }
            else if (drugi == 2)
            {
                Console.Clear();
                Console.WriteLine("-- za povratak na glavni izbornik upisite \"0\" --");
                Console.WriteLine("a. Po imenu\nb. Svih onih koji imaju više od 65 godina");
                do
                {
                    treci = Console.ReadLine();
                    switch (treci)
                    {
                        case "a":
                            Console.WriteLine("Kojeg radnika zelite izbrisati?");
                            var ime = Console.ReadLine();
                            radnici.Remove(ime);
                            treci = "0";
                            break;
                        case "b":
                            for (int i = 0; i < radnici.Count; i++)
                            {
                                foreach (var item in radnici)
                                {
                                    if (DateTime.Now.Year - item.Value.Year >65)
                                    {
                                        radnici.Remove(item.Key);
                                        break;
                                    }
                                }
                            }
                            treci = "0";
                            break;
                        default:
                            Console.WriteLine("Unesi tocnu vrijednost!");
                            break;
                    }
                } while (treci != "0");
            }
            else if (drugi == 3)
            {
                Console.Clear();
                Console.WriteLine("-- za povratak na glavni izbornik upisite \"0\" --");
                Console.WriteLine("Kojeg radnika zelite urediti? ");
                var ime=Console.ReadLine();
                Console.WriteLine("Unesi novi datum rodenja: ");
                radnici[ime]=datum();
            }
            else if (drugi == 4)
            {
                Console.Clear();
                Console.WriteLine("-- za povratak na glavni izbornik upisite \"0\" --");
                Console.WriteLine("a. svih radnika (format: ime - godine)\nb. svih radnika kojima je rođendan u tekućem mjesecu");
                do
                {
                    treci = Console.ReadLine();
                    switch (treci)
                    {
                        case "a":
                            foreach (var item in radnici)
                                Console.WriteLine($"{item.Key} - {item.Value}");
                            treci = "0";
                            break;
                        case "b":
                            foreach (var item in radnici)
                                if(item.Value.Month== DateTime.Now.Month)
                                    Console.WriteLine($"{item.Key} - {item.Value}");
                            treci = "0";
                            break;
                        default:
                            Console.WriteLine("Upisi tocnu vrijednost!");
                            break;
                    }
                } while (treci != "0");
                var izlaz = Console.ReadLine();
            }
        } while (drugi!=0);
    }






    else if(prvi ==3)
    {
        do
        {
            Console.Clear();
            Console.WriteLine("-- za povratak na glavni izbornik upisite \"0\" --");
            Console.WriteLine("1. Unos novog računa\n2. Ispis");
            do
            {
                if (int.TryParse(Console.ReadLine(), out drugi) && 3 > drugi && drugi >= 0)
                    break;
                Console.WriteLine("Upisi odgovarajucu vrijednost");
            } while (true);
            if (drugi == 1)
            {
                Console.WriteLine("-- Lista dostupnik artikla --");
                foreach (var item in listaArtikla)
                    Console.WriteLine(item);
                Console.WriteLine("-- Ako ste gotovi sa upisivanjem artikla, upisite: \"gotovo\" --");
                var proizvod = "";
                int kol=1;
                do
                {
                    Console.WriteLine("Unesite ime artikal za kupnju: ");
                    proizvod = Console.ReadLine();
                    foreach (var item in listaArtikla)
                    {
                        if(item.Name == proizvod)
                        {
                            Console.WriteLine("Unesite kolicinu artikla za kupnju: ");
                            do
                            {
                                if (!int.TryParse(Console.ReadLine(), out kol))
                                {
                                    Console.WriteLine("Upisi odgovarajucu vrijednost");
                                }
                                else if (kol > item.Quant)
                                {
                                    Console.WriteLine("Vrijednost je pre velika");
                                }
                                else
                                    break;
                            } while (true);
                            listaZaRacune.Add((proizvod, kol, item.Price));
                            listaArtikla2.Add((item.Name, item.Quant - kol, item.Price, item.Date));
                        }
                    }
                    foreach (var item in listaArtikla)
                    {
                        if (proizvod.ToLower() == "gotovo")
                            break;
                        if (item.Name == listaArtikla2[0].Name)
                        {
                            listaArtikla.Remove(item);
                            listaArtikla.Add(listaArtikla2[0]);
                            break;
                        }    
                    }
                    listaArtikla2.Clear();
                } while (proizvod.ToLower() != "gotovo");
                Console.WriteLine("-- Vas racun --");
                foreach (var item in listaZaRacune)
                    Console.WriteLine(item);
                Console.WriteLine("-- Ako ste zadovoljni sa racunom upisite 1, ako niste upisite drugi broj --");
                var odg = 0;
                do
                {
                    if (int.TryParse(Console.ReadLine(), out odg))
                        break;
                    Console.WriteLine("Upisi odgovarajucu vrijednost");
                } while (true);
                if(odg==1)
                {
                    id++;
                    racuni[(id, DateTime.Now)] = listaZaRacune;
                    listaZaRacune.Clear();
                    Console.Clear();
                    foreach (var item in racuni)
                    {
                        var kljuc = item.Key;
                        var vrijednost = item.Value;
                        if(kljuc.Item1==id)
                        {
                            float ukupno = 0;
                            Console.WriteLine($"id: {kljuc.Item1}\t\t\t datum: {kljuc.Item2}");
                            foreach (var sus in vrijednost)
                            {
                                Console.WriteLine(sus);
                                Console.WriteLine($"{sus.Name} {sus.Quant} {sus.Price}");
                                ukupno += sus.Price;
                            }
                            Console.WriteLine($"\n\nUkupna cijena: {ukupno}");
                            Console.ReadKey();
                        }
                    }
                }

            }
            
            else if (drugi == 2)
            {
                Console.Clear();
                Console.WriteLine("-- za povratak na glavni izbornik upisite \"0\" --");
                Console.WriteLine("a. Svih artikala kako su spremljeni\nb. Svih artikala sortirano po imenu\n" +
                    "c. Svih artikala sortirano po datumu silazno\nd. Svih artikala sortirano po datumu uzlazno\n" +
                    "e. Svih artikala sortirano po količini");
                do
                {
                    treci = Console.ReadLine();
                    switch (treci)
                    {
                        case "a":
                            foreach (var item in listaArtikla)
                                Console.WriteLine($"{item}");
                            treci = "0";
                            break;
                        case "b":
                            listaArtikla2.AddRange(listaArtikla.OrderBy(x => x.Name).ToList());
                            foreach (var item in listaArtikla2)
                                Console.WriteLine($"{item}");
                            listaArtikla2.Clear();
                            treci = "0";
                            break;
                        case "c":
                            listaArtikla2.AddRange(listaArtikla.OrderBy(x => x.Date).ToList());
                            listaArtikla2.Reverse();
                            foreach (var item in listaArtikla2)
                                Console.WriteLine($"{item}");
                            listaArtikla2.Clear();
                            treci = "0";
                            break;
                        case "d":
                            listaArtikla2.AddRange(listaArtikla.OrderBy(x => x.Date).ToList());
                            foreach (var item in listaArtikla2)
                                Console.WriteLine($"{item}");
                            listaArtikla2.Clear();
                            treci = "0";
                            break;
                        case "e":
                            listaArtikla2.AddRange(listaArtikla.OrderBy(x => x.Quant).ToList());
                            foreach (var item in listaArtikla2)
                                Console.WriteLine($"{item}");
                            listaArtikla2.Clear();
                            treci = "0";
                            break;
                        default:
                            Console.WriteLine("Upisi tocnu vrijednost!");
                            break;
                    }
                } while (treci != "0");
                var izlaz = Console.ReadLine();
            }

        } while (drugi != 0);
    }
    else if (prvi == 0)
        break;
} while (true);











static (string, int, float, DateOnly) noviArtikl()
{
    Console.WriteLine("Upisi ime artikla: ");
    var name=Console.ReadLine();

    Console.WriteLine("Upisite kolicinu artikala: ");
    var quant = 1;
    do
    {
        if (int.TryParse(Console.ReadLine(), out quant))
            break;
        Console.WriteLine("Upisi odgovarajucu vrijednost");
    } while (true);

    Console.WriteLine("Upisite cijenu artikla: ");
    var price=(float)1;
    do
    {
        if (float.TryParse(Console.ReadLine(), out price))
            break;
        Console.WriteLine("Upisi odgovarajucu vrijednost");
    } while (true);

    var date= datum();
    var tuple = (name, quant, price, date);
    return tuple;
}



static DateOnly datum()
{
    Console.WriteLine("Unesi godinu: ");
    var year = 1;
    do
    {
        if (int.TryParse(Console.ReadLine(), out year))
            break;
        Console.WriteLine("Upisi odgovarajucu vrijednost");
    } while (true);

    Console.WriteLine("Unesi mjesec: ");
    var month = 1;
    do
    {
        if (int.TryParse(Console.ReadLine(), out month) && month>0 && month <13)
            break;
        Console.WriteLine("Upisi odgovarajucu vrijednost");
    } while (true);

    Console.WriteLine("Unesi dan: ");
    var day = 1;
    do
    {
        if (int.TryParse(Console.ReadLine(), out day) && day >0 && day<32)
            break;
        Console.WriteLine("Upisi odgovarajucu vrijednost");
    } while (true);

    var date = new DateOnly(year, month, day);
    return date;
}