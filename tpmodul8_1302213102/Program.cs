using System.Text.Json;

internal class Program
{
    public class CovidConfig
    {
        public Konfig konfig;
        private const string filepath = @"covid_config.json";

        public CovidConfig()
        {
            try
            {
                ReadKonfigFile();
            }
            catch
            {
                SetDefault();
                WriteKonfigFile();
            }
        }
        public void ReadKonfigFile()
        {
            string hasilBaca = File.ReadAllText(filepath);
            konfig = JsonSerializer.Deserialize<Konfig>(hasilBaca);
        }
        public void WriteKonfigFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            string tulisan = JsonSerializer.Serialize(konfig, options);
            File.WriteAllText(filepath, tulisan);
        }
        public void SetDefault()
        {
            konfig = new Konfig();
            konfig.satuan_suhu = "celcius";
            konfig.batas_hari_demam = 14;
            konfig.pesan_ditolak = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
            konfig.pesan_diterima = "Anda dipersilahkan untuk masuk ke dalam gedung ini";
        }
        public void UbahSatuan()
        {
            konfig.satuan_suhu = (konfig.satuan_suhu == "celcius") ?
                "fahrenheit" : "celcius";
            WriteKonfigFile();
        }
    }
    public class Konfig
    {
        public string satuan_suhu { get; set; }
        public int batas_hari_demam { get; set; }
        public string pesan_ditolak { get; set; }
        public string pesan_diterima { get; set; }
        public Konfig() { }
    }
    private static void Main(string[] args)
    {
        CovidConfig covig = new CovidConfig();

        Console.Write("Berapa suhu badan anda saat ini?" +
            " Dalam nilai " + covig.konfig.satuan_suhu + " ");
        double suhu = Convert.ToDouble(Console.ReadLine());

        Console.Write("Berapa hari yang lalu (perkiraan) " +
            "anda terakhir memiliki gejala demam? ");
        int lama_demam = Convert.ToInt32(Console.ReadLine());

        string pesan_keluar = covig.konfig.pesan_ditolak;
        if (lama_demam > covig.konfig.batas_hari_demam)
        {
            if (covig.konfig.satuan_suhu == "celcius")
            {
                pesan_keluar = (suhu >= 36.5 && suhu <= 37.5) ?
                    covig.konfig.pesan_diterima : covig.konfig.pesan_ditolak;
            }
            else if (covig.konfig.satuan_suhu == "fahrenheit")
            {
                pesan_keluar = (suhu >= 97.7 && suhu <= 99.5) ?
                    covig.konfig.pesan_diterima : covig.konfig.pesan_ditolak;
            }
        }
        Console.WriteLine(pesan_keluar);
        Console.WriteLine();
        Console.WriteLine("--------------Menjalankan method UbahSatuan--------------");
        Console.WriteLine();

        covig.UbahSatuan();
        Console.Write("Berapa suhu badan anda saat ini?" +
            " Dalam nilai " + covig.konfig.satuan_suhu + " ");
        suhu = Convert.ToDouble(Console.ReadLine());

        Console.Write("Berapa hari yang lalu (perkiraan) " +
            "anda terakhir memiliki gejala demam? ");
        lama_demam = Convert.ToInt32(Console.ReadLine());

        pesan_keluar = covig.konfig.pesan_ditolak;
        if (lama_demam > covig.konfig.batas_hari_demam)
        {
            if (covig.konfig.satuan_suhu == "celcius")
            {
                pesan_keluar = (suhu >= 36.5 && suhu <= 37.5) ?
                    covig.konfig.pesan_diterima : covig.konfig.pesan_ditolak;
            }
            else if (covig.konfig.satuan_suhu == "fahrenheit")
            {
                pesan_keluar = (suhu >= 97.7 && suhu <= 99.5) ?
                    covig.konfig.pesan_diterima : covig.konfig.pesan_ditolak;
            }
        }
        Console.WriteLine(pesan_keluar);
    }
}