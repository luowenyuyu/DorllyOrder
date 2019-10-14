using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;

namespace ParkingOrder
{
    [RunInstaller(true)]
    public partial class ParkingInstaller : System.Configuration.Install.Installer
    {
        public ParkingInstaller()
        {
            InitializeComponent();
        }
    }
}
