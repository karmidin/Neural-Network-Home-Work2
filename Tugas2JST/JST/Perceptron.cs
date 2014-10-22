using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JST
{
    class Perceptron
    {
        public List<List<Pattern>> ListFont;
        public List<Pattern> ListPattern1, ListPattern2, ListPattern3;


        string hasilTraining, hasilTesting, printExcel;
        int alpa = 1;
        float threshold = 0.5f;
        int[] biasAkhir;
        float[,] wakhir;
        char huruf;
        int bias;

        public Perceptron()
        {
            ListFont = new List<List<Pattern>>();
            ListPattern1 = new List<Pattern>();
            ListPattern2 = new List<Pattern>();
            ListPattern3 = new List<Pattern>();
            wakhir = new float[21, 63];
            huruf = new char();
            biasAkhir = new int[21];

        }


        public void setTarget(List<Pattern> ListPattern, int c)
        {
            switch (c)
            {
                case 0:
                    ListPattern[0].t = 1;
                    ListPattern[1].t = -1;
                    ListPattern[2].t = -1;
                    ListPattern[3].t = -1;
                    ListPattern[4].t = -1;
                    ListPattern[5].t = -1;
                    ListPattern[6].t = -1;
                    huruf = 'A';
                    break;
                case 1:
                    ListPattern[0].t = -1;
                    ListPattern[1].t = 1;
                    ListPattern[2].t = -1;
                    ListPattern[3].t = -1;
                    ListPattern[4].t = -1;
                    ListPattern[5].t = -1;
                    ListPattern[6].t = -1;
                    huruf = 'B';
                    break;
                case 2:
                    ListPattern[0].t = -1;
                    ListPattern[1].t = -1;
                    ListPattern[2].t = 1;
                    ListPattern[3].t = -1;
                    ListPattern[4].t = -1;
                    ListPattern[5].t = -1;
                    ListPattern[6].t = -1;
                    huruf = 'C';
                    break;
                case 3:
                    ListPattern[0].t = -1;
                    ListPattern[1].t = -1;
                    ListPattern[2].t = -1;
                    ListPattern[3].t = 1;
                    ListPattern[4].t = -1;
                    ListPattern[5].t = -1;
                    ListPattern[6].t = -1;
                    huruf = 'D';
                    break;
                case 4:
                    ListPattern[0].t = -1;
                    ListPattern[1].t = -1;
                    ListPattern[2].t = -1;
                    ListPattern[3].t = -1;
                    ListPattern[4].t = 1;
                    ListPattern[5].t = -1;
                    ListPattern[6].t = -1;
                    huruf = 'E';
                    break;
                case 5:
                    ListPattern[0].t = -1;
                    ListPattern[1].t = -1;
                    ListPattern[2].t = -1;
                    ListPattern[3].t = -1;
                    ListPattern[4].t = -1;
                    ListPattern[5].t = 1;
                    ListPattern[6].t = -1;
                    huruf = 'J';
                    break;
                case 6:
                    ListPattern[0].t = -1;
                    ListPattern[1].t = -1;
                    ListPattern[2].t = -1;
                    ListPattern[3].t = -1;
                    ListPattern[4].t = -1;
                    ListPattern[5].t = -1;
                    ListPattern[6].t = 1;
                    huruf = 'K';
                    break;
            }

        }

        public string Testing()
        {

            hasilTesting += "Hasil Testing\n";
            printExcel += "\nHasil Testing\n";

            int countFont = 1, countWakhir = 0;

            foreach (var items in ListFont)
            {

                for (int i = 0; i < 7; i++)
                {
                    setTarget(items, i);
                    hasilTesting += "\nFont Ke " + countFont + "\n";
                    hasilTesting += "\nMengenali Huruf " + huruf + "\n";
                    hasilTesting += "\nNilai Bobot Akhir" + "\n";

                    printExcel += "\nFont Ke " + countFont + "\n";
                    printExcel += "\nMengenali Huruf " + huruf + "\n";
                    printExcel += "\nNilai Bobot Akhir" + "\n";

                    for (int k = 0; k < 63; k++)
                    {
                        printExcel = printExcel + "W" + (k + 1) + ",";
                    }
                    printExcel += "\n";
                    for (int j = 0; j < 63; j++)
                    {
                        if ((j + 1) % 63 == 0)
                        {

                            hasilTesting += wakhir[countWakhir, j] + "\n";
                            printExcel += wakhir[countWakhir, j] + "\n";
                        }
                        else
                        {

                            hasilTesting += wakhir[countWakhir, j];
                            printExcel += wakhir[countWakhir, j] + ",";
                        }
                    }

                    hasilTesting += "\nNilai Net | ";
                    printExcel += "\nNet" + ":,";
                    foreach (var item in items)
                    {

                        for (int k = 0; k < item.x.Length; k++)
                        {
                            item.net = item.net + (item.x[k] * wakhir[countWakhir, k]);
                        }

                        item.net = item.net + biasAkhir[countWakhir];
                        hasilTesting += item.net + " | ";
                        printExcel += item.net + ",";
                        item.net = 0;
                    }

                    hasilTesting += "\nBias Akhir : " + biasAkhir[countWakhir] + "\n";
                    printExcel += "\nBias Akhir :, " + biasAkhir[countWakhir] + "\n";

                    hasilTesting += "\n";
                    printExcel += "\n";
                    countWakhir++;
                }
                countFont++;
            }

            return hasilTesting;
        }

        public string Training()
        {
            int countFont = 1, countWakhir = 0;
            hasilTraining += "Hasil Training\n";
            printExcel += "Hasil Training\n";
            foreach (var items in ListFont)
            {

                for (int j = 0; j < 7; j++)
                {
                    int countEpoch = 1;
                    bool belajar = true;
                    //awal = true;

                    setTarget(items, j);
                    hasilTraining += "\nFont Ke " + countFont + "\n";
                    hasilTraining += "\nMengenali Huruf " + huruf + "\n";

                    printExcel += "\nFont Ke " + countFont + "\n";
                    printExcel += "\nMengenali Huruf " + huruf + "\n";
                    while (isLearn())
                    {

                        hasilTraining += "\nEpoch Ke " + countEpoch + "\n";
                        hasilTraining += "\nNilai Bobot Baru\n";

                        printExcel += "\nEpoch Ke " + countEpoch + "\n";
                        printExcel += "\nNilai Bobot Baru\n";
                        for (int i = 0; i < 63; i++)
                        {
                            printExcel += "W" + (i + 1) + ",";
                        }
                        printExcel += "\n";
                        foreach (var item in items)
                        {

                            for (int k = 0; k < item.x.Length; k++)
                            {

                                item.net = item.net + (item.x[k] * wakhir[countWakhir, k]);
                            }


                            item.net = item.net + bias;


                            if (item.net > threshold)
                            {
                                item.fnet = 1;

                            }
                            else if (item.net >= 0 && item.net <= threshold)
                            {
                                item.fnet = 0;

                            }
                            else
                            {
                                item.fnet = -1;

                            }


                            if (item.fnet != item.t)
                            {
                                belajar = true;
                                bias = bias + (1 * item.t * alpa);
                            }
                            else
                            {
                                belajar = false;
                            }



                            for (int k = 0; k < item.x.Length; k++)
                            {
                                if (belajar == true)
                                {
                                    item.w[k] = item.x[k] * item.t * alpa;
                                    wakhir[countWakhir, k] = item.w[k] + wakhir[countWakhir, k];

                                }
                                else
                                {
                                    item.w[k] = 0;
                                }


                                if ((k + 1) % 63 == 0)
                                {
                                    hasilTraining += wakhir[countWakhir, k] + "\n";
                                    printExcel += wakhir[countWakhir, k] + "\n";
                                }
                                else
                                {
                                    hasilTraining += wakhir[countWakhir, k];
                                    printExcel += wakhir[countWakhir, k] + ",";
                                }
                            }
                            item.b = bias;
                        }

                        hasilTraining += "\nNilai Bias" + " | ";
                        printExcel += "\nBias" + ":,";
                        foreach (var item in items)
                        {
                            hasilTraining += item.b + " | ";
                            printExcel += item.b + ",";
                        }

                        hasilTraining += "\nNilai Net" + " | ";
                        printExcel += "\nNet" + ":,";
                        foreach (var item in items)
                        {
                            hasilTraining += item.net + " | ";
                            printExcel += item.net + ",";
                        }

                        hasilTraining += "\nNilai Fnet" + " | ";
                        printExcel += "\nFnet" + ":,";
                        foreach (var item in items)
                        {
                            hasilTraining += item.fnet + " | ";
                            printExcel += item.fnet + ",";
                        }

                        hasilTraining += "\nNilai Target" + " | ";
                        printExcel += "\nTarget" + ":,";
                        foreach (var item in items)
                        {
                            hasilTraining += item.t + " | ";
                            printExcel += item.t + ",";
                        }

                        hasilTraining += "\n";
                        printExcel += "\n";
                        countEpoch++;
                    }
                    biasAkhir[countWakhir] = bias;
                    countWakhir++;
                    Console.WriteLine(bias);
                    bias = 0;
                }
                countFont++;
            }
            return hasilTraining;
        }


        public bool isLearn()
        {
            foreach (var items in ListFont)
            {
                foreach (var item in items)
                {
                    if (item.t != item.fnet)

                        return true;
                }
            }
            foreach (var items in ListFont)
            {
                foreach (var item in items)
                {
                    //item.b = 0;
                    item.net = 0;
                    //item.fnet = 0;
                }
            }
            return false;
        }

        public void Reset()
        {
            hasilTesting = "";
            hasilTraining = "";
            printExcel = "";
            ListFont.Clear();
            ListPattern1.Clear();
            ListPattern2.Clear();
            ListPattern3.Clear();
            biasAkhir = new int[21];
            wakhir = new float[21, 63];
            bias = 0;
        }

        public string getPrintToExcel()
        {
            int countFont = 1;
            string input = "";

            foreach (var items in ListFont)
            {
                input += "Font ke" + countFont + "\n";

                for (int i = 0; i < 63; i++)
                {
                    input = input + "X" + (i + 1) + ",";
                }
                input += "\n";
                foreach (var item in items)
                {

                    for (int i = 0; i < item.x.Length; i++)
                    {
                        input = input + item.x[i] + ",";
                    }

                    input += "\n";

                }
                countFont++;
            }
            printExcel = input + printExcel;
            return printExcel;
        }

    }
}
