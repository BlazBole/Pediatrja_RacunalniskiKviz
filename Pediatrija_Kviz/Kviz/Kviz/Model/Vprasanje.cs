using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kviz
{
    public class Vprasanje
    {
        public string VprasanjeTekst { get; set; }

        public List<Odgovor> Odgovori { get; set; }
    }

    public class Odgovor 
    { 
        public string OdgovorTekst { get; set; }
        public bool Pravilen { get; set; }
    }

}
