using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{

    internal class ViewModelMain : BaseViewModel
    {
        private ModelApi model;

        public ViewModelMain() 
        {
            model = new Model.ModelApi();
        }

        public void Start()
        { 
            
        }
    }
}
