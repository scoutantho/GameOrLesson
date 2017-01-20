using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOrLesson
{
    class Base
    {
        private String nom;
        private String chemin;
        private Boolean onlyFiles = false;

        public Base(String nom, String chemin)
        {
            this.nom = nom;
            this.chemin = chemin;
        }
        public Base(String nom, String chemin,Boolean onlyFiles)
        {
            this.nom = nom;
            this.chemin = chemin;
            this.onlyFiles = onlyFiles;
        }

        public String getNom { get { return nom; } }
        public String getChemin { get { return chemin; } }
        public Boolean getOnlyFiles { get { return onlyFiles; } }

    }
}
