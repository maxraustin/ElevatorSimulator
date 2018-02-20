﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorApp.Core.Interfaces;
using ElevatorApp.Core.Models;

namespace ElevatorApp.Core.Models
{
    public class ButtonPanel : ICollection<FloorButton>, IButtonPanel
    {
        public ICollection<FloorButton> FloorButtons { get; set; } = new ObservableCollection<FloorButton>
        {
            new FloorButton(4),
            new FloorButton(3)
#if DEBUG
            {
                Active=true
            }
#endif
            ,
            new FloorButton(2),
            new FloorButton(1),
        };

        public DoorButton OpenDoorButton { get; set; } = DoorButton.Open;
        public DoorButton CloseDoorButton { get; set; } = DoorButton.Close;

        #region ICollectionImplementation
        public IEnumerator<FloorButton> GetEnumerator()
        {
            return FloorButtons.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)FloorButtons).GetEnumerator();
        }

        public void Add(FloorButton item)
        {
            FloorButtons.Add(item);
        }

        public void Clear()
        {
            FloorButtons.Clear();
        }

        public bool Contains(FloorButton item)
        {
            return FloorButtons.Contains(item);
        }

        void ICollection<FloorButton>.CopyTo(FloorButton[] array, int arrayIndex)
        {
            FloorButtons.CopyTo(array, arrayIndex);
        }

        public bool Remove(FloorButton item)
        {
            return FloorButtons.Remove(item);
        }

        public int Count => FloorButtons.Count;

        public bool IsReadOnly => FloorButtons.IsReadOnly;
        #endregion

        public void Subscribe(ElevatorMasterController controller)
        {
            foreach (FloorButton button in this.FloorButtons)
            {
                button.OnPushed += (a, b) => controller.QueueElevator(button.FloorNum);
            }
        }
    }
}