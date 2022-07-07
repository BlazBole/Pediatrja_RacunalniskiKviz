using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kviz
{
    public partial class Form1 : Form
    {
        int _stevecVsehOdgovorov = 0;
        int _stevecPravilnihOdgovorov = 0;
        List<Vprasanje> _vprasanja;
        int _stevecVprasanj = 0;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NapolniVprasanja();

            label1.Text = _stevecPravilnihOdgovorov.ToString();

            _vprasanja = _vprasanja.OrderBy(x => Guid.NewGuid()).ToList();
            foreach (Vprasanje o in _vprasanja)
                o.Odgovori = o.Odgovori.OrderBy(x => Guid.NewGuid()).ToList();


            Vprasanje tmp = _vprasanja[_stevecVprasanj];
            lblVprasanje.Text = tmp.VprasanjeTekst;

            CheckBox c;
            foreach(var o in tmp.Odgovori)
            {
                c = new CheckBox();
                c.Text = o.OdgovorTekst;
                c.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                c.Checked = false;
                c.AutoSize = true;
                c.Location = new Point(50, tmp.Odgovori.IndexOf(o) * 40);
                c.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
                c.Name = "chk" + tmp.Odgovori.IndexOf(o);
                c.Tag = tmp.Odgovori.IndexOf(o);
                panel1.Controls.Add(c);
                if (o.Pravilen)
                    _stevecVsehOdgovorov++;
            }    
        }

        private void btnNaprej_Click(object sender, EventArgs e)
        {
            foreach(Control con in panel1.Controls)
                if(con.GetType() == typeof(CheckBox))
                    if(((CheckBox)con).Checked && _vprasanja[_stevecVprasanj].Odgovori[Convert.ToInt32(con.Tag)].Pravilen)
                        _stevecPravilnihOdgovorov++;


            if (_stevecVprasanj >= _vprasanja.Count() - 1)
            {
                int solskaOcena = 0;
                double ocenaProcenti = ((_stevecPravilnihOdgovorov * 100) / (_stevecVsehOdgovorov));

                if(ocenaProcenti <= 55)
                {
                    solskaOcena = 5;
                    MessageBox.Show("Pisali ste: " + Convert.ToString(Math.Round(ocenaProcenti, 2)) + "%\r\n" + "Ocena: " + Convert.ToString(solskaOcena) + "(nzd), Nisi dovolj pripravljen/a za izpit!");
                }
                else if(ocenaProcenti >= 56 && ocenaProcenti <= 65)
                {
                    solskaOcena = 6;
                    MessageBox.Show("Pisali ste: " + Convert.ToString(Math.Round(ocenaProcenti, 2)) + "%\r\n" + "Ocena: " + Convert.ToString(solskaOcena) + "(zd), Predlagam ti, da še kviz vsaj enkrat ponoviš!");
                }
                else if(ocenaProcenti >= 66 && ocenaProcenti <= 75)
                {
                    solskaOcena = 7;
                    MessageBox.Show("Pisali ste: " + Convert.ToString(Math.Round(ocenaProcenti, 2)) + "%\r\n" + "Ocena: " + Convert.ToString(solskaOcena) + "(db), Na izpit si pripravljena/a dobro, vendar še en poskus ti nebi skodil ;).");
                }
                else if(ocenaProcenti >= 76 && ocenaProcenti <= 85)
                {
                    solskaOcena = 8;
                    MessageBox.Show("Pisali ste: " + Convert.ToString(Math.Round(ocenaProcenti, 2)) + "%\r\n" + "Ocena: " + Convert.ToString(solskaOcena) + "(pdb), Čestitam, kviz si rešil prav dobro, če misliš, da si zaslužiš višjo oceno, ponovno reši kviz!.");
                }
                else if(ocenaProcenti >= 86 && ocenaProcenti <= 93)
                {
                    solskaOcena = 9;
                    MessageBox.Show("Pisali ste: " + Convert.ToString(Math.Round(ocenaProcenti, 2)) + "%\r\n" + "Ocena: " + Convert.ToString(solskaOcena) + "(zdb), WOOOW ,Na izpit si pripravljena/a zelo dobro, zagotovo boš uspešen/a.");
                }
                else if(ocenaProcenti >= 94)
                {
                    solskaOcena = 10;
                    MessageBox.Show("Pisali ste: " + Convert.ToString(Math.Round(ocenaProcenti, 2)) + "%\r\n" + "Ocena: " + Convert.ToString(solskaOcena) + "(odl),WOOOOOOW, Na izpit si pripravljena/a vrhunsko, čestitam, izpit bo zagotovo šel kot po maslu!");
                }
                else
                    MessageBox.Show("");




                _stevecVprasanj = -1;
                _stevecPravilnihOdgovorov = 0;
                MessageBox.Show("Konec vprašanj!\r\nZačel se bo novi krog vprašanj!");
            }

            
            panel1.Controls.Clear();
            _stevecVprasanj++;
            Vprasanje tmp = _vprasanja[_stevecVprasanj];
            lblVprasanje.Text = tmp.VprasanjeTekst;
            CheckBox c;
            foreach (var o in tmp.Odgovori)
            {
                c = new CheckBox();
                c.Text = o.OdgovorTekst;
                c.Checked = false;
                c.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                c.AutoSize = true;
                c.Location = new Point(50, tmp.Odgovori.IndexOf(o) * 50);
                c.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
                c.Name = "chk" + tmp.Odgovori.IndexOf(o);
                c.Tag = tmp.Odgovori.IndexOf(o);
                c.CausesValidation = true;
                panel1.Controls.Add(c);

                if (o.Pravilen)
                    _stevecVsehOdgovorov++;
            }

            label1.Text = "Točke: " + _stevecPravilnihOdgovorov.ToString();

            
        }

        private void btnPreveri_Click(object sender, EventArgs e)
        {
            foreach (Control c in panel1.Controls)
                if(c.GetType().Name == "CheckBox")
                checkBox1_CheckedChanged(c, new CancelEventArgs());
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool pravilenOdg = false;
            CheckBox c = (CheckBox)sender;
            string t = c.Tag.ToString();
            Vprasanje tmp = _vprasanja[_stevecVprasanj];
            if (tmp.Odgovori[Convert.ToInt32(t)].Pravilen)
                pravilenOdg = true;

            PictureBox p = new PictureBox();
            if (pravilenOdg)
                p.Image = global::Kviz.Properties.Resources.corectIcon;
            else
                p.Image = global::Kviz.Properties.Resources.incorectIcon;

            p.Location = new System.Drawing.Point(0, c.Location.Y);
            p.Name = "pictureBox" + c.Tag;
            p.Size = new System.Drawing.Size(28, 26);
            p.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            panel1.Controls.Add(p);
        }

        #region Vprašanja

        private void NapolniVprasanja()
        {
            _vprasanja = new List<Vprasanje>();

            _vprasanja.Add(new Vprasanje() {
                VprasanjeTekst = "1. Zahirančki so?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() { 
                            OdgovorTekst = "Novorojenčki, ki so rojeni s porodno težo, ki je pod 8. percentilo za gestacijsko starost.", 
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Novorojenčki, ki so rojeni s porodno težo, ki je pod 10. percentilo za gestacijsko starost.",
                            Pravilen = true
                        },
                         new Odgovor() {
                            OdgovorTekst = "Novorojenčki, ki so rojeni s porodno težo, ki je pod 10. percentilo za pomenstruacijsko leto.",
                            Pravilen = false
                        }
                    }
                }
            );


            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "2. Med pogostejše ob porodne poškodbe sodijo.",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Zlom kjlučnice, Kefalhemathom, poškodba presredka I. stopnje.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Zlom kjlučnice, Kefalhemathom, Caputsuccedaneum.",
                            Pravilen = true
                        },
                           new Odgovor() {
                            OdgovorTekst = "Tumorji medenice (posebno jajčnikov, redko miomov, razen cervikalnih), Kefalhemathom, Caputsuccedaneum.",
                            Pravilen = false
                        },
                            new Odgovor() {
                            OdgovorTekst = "Tumorji medenice (posebno jajčnikov, redko miomov, razen cervikalnih), Kefalhemathom, poškodba hrbtenice.",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "3. Razlika pri oživljanju dojenčkov in velikih otrok?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Položaj celotnega telesa.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Kontrola pulza",
                            Pravilen = true
                        },
                           new Odgovor() {
                            OdgovorTekst = "Položaj glave.",
                            Pravilen = true
                        },
                            new Odgovor() {
                            OdgovorTekst = "Ležanje na desni strani boka.",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "4. Znaki dihalne stiske?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Pospešeno dihanje.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Znaki povečanega dihalnega dela.",
                            Pravilen = true
                        },
                           new Odgovor() {
                            OdgovorTekst = "Stokanje, dispenja, cianoza.",
                            Pravilen = true
                        },
                            new Odgovor() {
                            OdgovorTekst = "Bruhanje, panični napadi.",
                            Pravilen = false
                        },
                            new Odgovor() {
                            OdgovorTekst = "Iregularno upočasnjeno dihanje in apneja.",
                            Pravilen = true
                        },
                            new Odgovor() {
                            OdgovorTekst = "Ugrezanje medrebrnih prostorov.",
                            Pravilen = true
                        }
                    }
                }
           );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "5. Kaj ocenjujemo z glasgow coma lestvico?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Stanje zavesti.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Hipertenzijo.",
                            Pravilen = false
                        },
                           new Odgovor() {
                            OdgovorTekst = "Položaj glave.",
                            Pravilen = false
                        },
                            new Odgovor() {
                            OdgovorTekst = "Stanje očesnih, gibalnih in govornih funkcij.",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "6.Sladkorna bolezen tip 1?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Njena pojavnost narašča.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Njena pojavnost pada.",
                            Pravilen = false
                        },
                           new Odgovor() {
                            OdgovorTekst = "Se ne zdravi z inzulinom",
                            Pravilen = false
                        },
                            new Odgovor() {
                            OdgovorTekst = "Se zdravi z inzulinom.",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "7. Okužbe dihalnih poti pri otrocih?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "So večinoma bakterijska obolenja.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "So večinoma virusna obolenja.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "So pogosto patologija v otroški dobi.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Nejpogostejši epiglotitis.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Kronična obstruktivna pljučna bolezen (KOPB).",
                            Pravilen = false
                        }
                    }
                }
            );


            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "8. Po Tannerjevih merilih klasificiramo?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Zaostanek v govornem razvoju pri otrocih.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Pubertetni razvoj.",
                            Pravilen = true
                        }
                    }
                }
            );


            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "9. Glavobol pri otrocih?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Imajo v več kot polovici tenzijski glavobol.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Je redko odraz organske patologije.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Pri zdravljenju so pomembni nefarmakološki ukrepi.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Je pogosto odraz organske patologije.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Je redko odraz anoorganske patologije.",
                            Pravilen = false
                        }
                    }
                }
            );



            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "10. Za prehrano dojenčka velja?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Živila uvajamo v tedenskih intervalih.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Živila uvajamo v mesečnih intervalih.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Živila uvajamo v dnevnih intervalih.",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "11. Med srčne napake brez cianoze sodi?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Defekt v pretinu.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Odprt Bottalov vod.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Defekt v pretinu prekatov in preddvorov.",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "12. Med preiskovalne metode v pediatrični kardiologiji sodi?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "MZ",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "UZ",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Obremenitveno testiranje na sobnem kolesu.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Rentgen.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Rentgenogram.",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "13. Za nedonošenčke velja?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Zaradi nezrelega možganskega žilja imajo pogosto možganske krvavitve.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Nezreli sesalni refleks",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Pomanjkanje sulfaktanta",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Pomanjkanje cinka",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "14. Kaj označuje družina?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Zaradi nezrelega možganskega žilja imajo pogosto možganske krvavitve.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Vse, ki zadevajo individualnega otroka.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Osnovno enoto človeške družbe.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Družina je ena najpomembnejših form v življenju posameznika in prva družbena skupina.",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "15. Kateri so cilji pediatrične ZN?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Promoviranje sodelovanja družine pri negi.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Skrb, je najboljša za otroka.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Promoviranje sodelovanja družine pri negi.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Omogočanje neodvisnosti in kontroliranje njihovega življenja.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Timsko delo v procesu zdravstvene nege.",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "16. Katere so k otroku usmerjene ZN? ",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Vključevanje njegove družine.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Celovitost negovalnih informacij.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Priznavanje pravic.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Omogočanje neodvisnosti in kontroliranje njihovega življenja.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Spodbujanje otroka in družine k sodelovanju.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Otrok pridobi občutek varnosti in izgubi občutek strahu.",
                            Pravilen = false
                        }
                    }
                }
           );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "17. Vključevanje igre v času? ",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Intelektualnemu razvoju.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Socialnemu razvoju.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Zmanjševanju negativnih posledic.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Pripravi otroka na izvajanje negovalnih intervencij.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Zmanjšati negativni hospitalizem.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "vzporedne igre, ki je pravzaprav samostojna igra otroka.",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "18. Dnevne bolnišnice delimo? ",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Psihiatrične.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Pediatrične.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Biološke.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Kemiološke",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "19. Kaj od naštetega spada med motnje spanja? (pravilni 3 od 4 odgovorov) ",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Somnabolizem.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Narkolepsija.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Depresija.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Apneja",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "20. Srčni utrip pri otroku lahko merite na",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Temporalni a.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Facialni a.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Subklaviji.",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "21. Najpomembnejši standardni ukrep ob osamitvi je? (pravilen 1 odgovor)",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Higiena telesa.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Pranje tekstilij.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Higiena rok.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Higiena prostorov.",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "22. S katerimi meritvami lahko opazujemo znake zakasnele rasti? ",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Merjenje glavice.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Merjenje teže/dolžine.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Merjenje rok.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Merjenje razpona rok.",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "23. Kakšne vrste izpuščajev poznamo? (Pozorno preberi odgovor)",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Pustula, Paula, Vezilula",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Makula, Krusta.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Pustula, Papula, Vezilula.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Eflorescenca, Makula, Krusta.",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "24. Zdravega otroka pri 3. mesecih starosti cepimo v okviru obveznega cepljenja za?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "DI-TE-PER-POLIO-HiB ter pnevmokok.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "TE-DI-PER-POL-HiB pnevmokok.",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "25. Zakaj prihaja pri otrocih z motnjami v duševnem razvoju do težav OŽA izločanje in odvajanje?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Zaradi zmanjšanega tonusa",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Zaradi zmanjševanja tonusa gladkih, trebušnih mišic.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Ne-gibanja.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Gibanja.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Nepomičnosti.",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "26. Febrilne konvulzije",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Vročinski krči.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Če napad po parih minutah ne mine med. th.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Pojavljajo se med 9. mesecom in 5. letom starosi.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "pojavljajo se med 6. mesecom in 5. letom starosi.",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "27. Prva pomoč pri zastrupitvi?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Skrb za lastno varnost pri reševanju, Klic na pomoč, Odstranjevanje strupa",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Vzpostavljanje in vzdrževanje osnovnih življenjskih funkcij, Prevoz zastrupljenca",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Prepoznavanje strupa in shranjevanje vzorca, Reševanje iz zastrupljenega območja.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Strup redčimo pri osebi z motnjo zavesti.",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "28. Najpogostejše bolezni v psihiatriji ? (pozorno preberi odgovore)",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Blodnjava motnja, Shizofrenija, ADDH.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "ADHD, Bipolarna motnja, Shizofrenija.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Motnje hranjenja, Obsesivno-kompenzivna motnja, Somatoformne motnje.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Motnje hranjenja, Obsesivno-kompenzivna motnja, Generalizirana anksiozna motnja.",
                            Pravilen = false
                        }
                    }
                }
           );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "29. Znaki dehidracije",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Podočnjaki, vdrte oči, suha usta, vdrt trebuh.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Gosta slina, obložen jezik, razpokane ustnice.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Razpokan jezik, bledica, zaspanost, vdrta fontanela oz. mečava pri dojenčkih",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Ledvica: oligurija, nefronoftiza, višja specifična teža urina,…",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Ledvica: oligurija, anurija, višja specifična teža urina,…",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "30. Zapleti pri i.m. aplikaciji zdravil?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Možnost naboda žil ali živcev.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Alergična reakcija.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Aseptična narkoza.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Aseptična nekroza.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Tvorba abscesa.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Zlom igle.",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "31. Prirojene anomalije pri Downovem sindromu",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Ozke, kratke dlani in prsti, klinodaktilija, brazda 3 prstov",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Široke, kratke dlani in prsti, brazda 4 prstov",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Male usta, nos, ušesa, ki so slabše oblikovana.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Poševno položene oči, hipertelorizem, epikantus, Brushfieldove pege.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Mentalna zaostalost, zastoj rasti, klinodaktilija",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Manjša glava, plosko zatilje.",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "32. Kolonizacija je",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "pomeni naselitev NO/parazitov na koži, sluznicah, organih.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "pomeni naselitev MO/parazitov na koži, sluznicah, organih.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "pomeni naselitev MO/parazitov na koži, želodčni kislini, organih.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "pomeni naselitev MO/parazitov na koži, sluznicah, notranjih organih.",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "33. Tipične otroške nalezljive bolezni ",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Otroška paraliza, oslovski kašelj, ošpice (izpuščaj), mumps, vnetje ušesa.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Oslovski kašelj, otroška paraliza, ošpice (izpuščaj), mumps.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "četrta bolezen (izpuščaj), peta bolezen (izpuščaj), rdečke (izpuščaj), škrlatinka (izpuščaj).",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "peta bolezen (izpuščaj), šesta bolezen (izpuščaj), rdečke (izpuščaj), norice (izpuščaj), vnetje ušesa.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "peta bolezen (izpuščaj), šesta bolezen (izpuščaj), rdečke (izpuščaj), norice (izpuščaj), škrlatinka (izpuščaj).",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "35. Označi pravilna dejstva o glutenu? ",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "optimalno je uvajati majhne količine glutena med 6. in 7. mesecem starosti otroka, ko ni več dojen.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "optimalno je uvajati majhne količine glutena med 6. in 7. mesecem starosti otroka, ko je še dojen.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "optimalno je uvajati majhne količine glutena med 5. in 8. mesecem starosti otroka, ko je še dojen.",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "37. Izključno dojenje je treba izvajati od 8 - 12 kra na dan.",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Pravilno.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Napacno.",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "38. Letno pridobivanje telesne teže/višine pri otrocih je največje?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "V prvih mesecih življenja.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "V puberteti.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Točno v petem letu starosti otroka.",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "39. Laktcijo spodbuja?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Orgazem",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "otroški jok.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "hidracija.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "sesanje.",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "40. Kateri od navedenih je pomemben podatek v nevrološki anamnezi? (Pravilen je 1 odgovor)",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Prisotnost meningealnih znakov, ki se pojavljajp v vzdraženja mening.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Opistotonus tilnika.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Ugriz klopa.",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "41. Med znake dihalnega napora spada?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Ugrezanje sternuma.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Dihanje z nosnimi krili.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Uporaba pomožnih mišic.",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "42. Pri astmi velja?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Značilne so epizode kašlja, težkega dihanja, piskanja.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Značilnost težkega poslabšanja.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Slab odgovor na zdravila.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Najbolj objektivna metoda za prepoznavo težje težjega napada je PEF.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Astma je obstruktivna pljučna bolezen.",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "43. Brushfieldove pege so značilne za?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Aspergerjev sindrom.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Gilbertov sindrom.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Budd-Chiarijev sindrom.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Downov sindrom.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Nič od naštetega.",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "44. Kaj vpliva na otrokovo doživljanje bolezni?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Trajanje.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Starost otroka.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Odnos staršev do otrokove bolezni.",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "45. Bipolarna motnja se kaže v? ",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Zlorabi drog.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Alkoholu.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Delikvenci.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Urinu.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Krvi.",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "47. Pri otroku lahko pride do febrilnih konvulzij ali?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Vročinskih krčev.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "ADHD.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Somnabolizma",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "48. Vodo pri kopanju ogrejemo na?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Med 30 in 35 stopinj celzija.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Natanko na 39 stopinj celzija",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Natanko na 37 stopinj celzija.",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "49. koliko je prirojenih srčnih napak?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "8/100",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "8 promilov.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "0,80 procentov.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "0,8 procentov.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "8/1000",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "50. Zdravljenje alergijskega nahoda?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Lok. Kortikostroidi",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Lok. Antihistaminiki",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Sistemsko antihistaminiki",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Zdravilo za postinflamatorno hiperpigmentacijo",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "51. Zakaj nuhalna svetlina?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Trisomija - Downov sindrom",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Srčne anomalije, plod z srčno napako",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Sistemsko antihistaminiki",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Motnja zaradi prisotnosti dodatnega kromosoma 20.",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "52. Diabetes TIP 1 zdravimo samo z inzulinom!",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Ne, saj za ta tip diabetesa ne obstaja zdravilo (priporočljiva dieta)",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Da",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "53. Vrste cianoz?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Anemična, stagnacijska, histotoksična, okužba sluznice",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Posinjelost kože zaradi pomanjkanja kisika v venozni krvi, stagnacijska, histotoksična, hipoksična",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Anemična, stagnacijska, histotoksična, hipoksična",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "54. Druga denticija?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Med 5. in 8. mesecem.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "med 7. in 9. letom.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Med 5. in 8. letom",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "55. Označi dejstva za APGAR?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "V prvi in peti uri.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "V prvi in peti minuti.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Največja možna ocena 10.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Ocenjuje se dihanje, barva kože, pulz.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Največja možna ocena 5.",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "56. Perinatalna umrljivost?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Je seštevek števila mrtvorojenih, težkih 100 g in več ter števila preživelih novorojenčkov, starih 0-6 dni in težki 100 g in več, na 1000 rojstev.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Je seštevek števila mrtvorojenih, težkih 1000 g in več ter števila umrlih novorojenčkov, starih 0-6 dni in težki 1000 g in več, na 1000 rojstev.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Leta 2006 je bila 6,1 ( IVZ ).",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "57. Dnevna diureza pri otroku nad enil letom?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "1dl/kg/h",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "1ml/kg/m",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "10ml/kg/h",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "1ml/kg/h",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "58. Značilnosti anorexia nervosa lahko ima faze nažiranja in bruhanja?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Da.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Ne",
                            Pravilen = false
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "59. Pozitivni učinki specifične imunoterapije in hipersenzibilizacije?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Stranski učinki imunoterapije so lahko manj verjetni, če bolnik hkrati jemlje več različnih imunoterapevtskih zdravil.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Zmanjša uporabo antialergijskih zdravil.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Sistem th 1 th 2.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Povzroči senzibilizacijo na nove alergene.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Prepreči senzibilizacijo na nove alergene.",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "61. Dolgotrajni zapleti SB:",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Nevropatija, nefropatija, dolgotrajno obolenje žilnega sistema.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Nevropatija, retinopatija, nefropatija, horesterol.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "retinopatija.",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "62. Najpogostejši prehrambeni alergeni?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Riž.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Beljak.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Jabolka",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Morski sadeži",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Arašidi",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "63. Bronhitis znaki so?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Vidni na zobnem rentgenu.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Vidni na RTG, kašelj(lahko produktiven), piskanje, vizing.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Telesna temperatura okoli 36.8 stopinj celzija.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Najpogostejše povzročitelje (RSV, adenoviruse) dokažemo z brisom nosu in žrela",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "63. Bronhitis znaki so?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Vidni na zobnem rentgenu.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Vidni na RTG, kašelj(lahko produktiven), piskanje, vizing.",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Telesna temperatura okoli 36.8 stopinj celzija.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Najpogostejše povzročitelje (RSV, adenoviruse) dokažemo z brisom nosu in žrela",
                            Pravilen = true
                        }
                    }
                }
            );

            _vprasanja.Add(new Vprasanje()
            {
                VprasanjeTekst = "64. Kaj velja za adrenalno krizo?",
                Odgovori = new List<Odgovor>() {
                        new Odgovor() {
                            OdgovorTekst = "Bruhanje, dehidracija, zvišanj kalij.",
                            Pravilen = false
                        },
                        new Odgovor() {
                            OdgovorTekst = "Hipotenzija, trebušne bolečine, utrujenost, hiperpigmentacija, ",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Krči, Hipoglikemija,",
                            Pravilen = true
                        },
                        new Odgovor() {
                            OdgovorTekst = "Bruhanje, dehidracija",
                            Pravilen = true
                        }
                    }
                }
            );




        }




        #endregion


    }
}
