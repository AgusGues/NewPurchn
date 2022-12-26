using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Factory;
using Domain;

namespace GRCweb1.Interfaces
{
    public interface IT3Simetris
    {
        List<Produk.PartnoStok> GetPartNoStock();
    }
}
