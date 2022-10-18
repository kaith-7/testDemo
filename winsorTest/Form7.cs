using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winsorTest
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            _container = new WindsorContainer();
            //_container.Kernel.ReleasePolicy = new TransientReleasePolicy(_container.Kernel);
        }
        private readonly WindsorContainer _container;
        private void Form7_Load(object sender, EventArgs e)
        {
           // _container = new Castle.Windsor.WindsorContainer();
            _container.Register(Component.For<Class1>().ImplementedBy<Class1>().LifestyleTransient ());

            var classx = _container.Resolve<Class1>();
    }
    }
}
