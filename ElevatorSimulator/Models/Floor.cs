﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ElevatorApp.Models.Interfaces;

namespace ElevatorApp.Models
{
    public class Floor : ModelBase, IFloor, ISubcriber<ElevatorMasterController>
    {
        public int FloorNumber { get; }


        public ICollection<ElevatorCallPanel> Buttons { get; } = new ObservableCollection<ElevatorCallPanel> { new ElevatorCallPanel() };

        public Floor(int floorNumber, IEnumerable<ElevatorCallPanel> buttons)
        {
            this.FloorNumber = floorNumber;
            this.Buttons = new ObservableCollection<ElevatorCallPanel>(buttons);
        }

        public Floor(int floorNumber, int callPanels = 1)
            : this(floorNumber, new ObservableCollection<ElevatorCallPanel>(Enumerable.Range(0, callPanels).Select(a => new ElevatorCallPanel(a))))
        {

        }

        public Floor()
        {

        }


        public bool Subscribed { get; private set; }

        public void Subscribe(ElevatorMasterController parent)
        {
            //if (Subscribed)
            //    return;

            foreach (ElevatorCallPanel panel in this.Buttons)
            {
                panel.Subscribe(parent);
            }
            this.Subscribed = true;
        }
    }
}