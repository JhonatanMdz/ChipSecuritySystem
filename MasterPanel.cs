using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    public class MasterPanel
    {
        private ColorChip[] _colorChips { get; set; }
        
        public MasterPanel(List<ColorChip> colorChips)
        {
            //An array is created from input list to have a better loop performance...
            this._colorChips = new ColorChip[colorChips.Count];
            colorChips.CopyTo(this._colorChips);
        }

        public void ValidateChipBag()
        {
            //falg tha indicates if the panel can be unlock.
            bool canUnlock = false;

            //Tracks the current starColor, initialized with blue marker...
            Color currentStart = Color.Blue;
            //Stores the desired end color...
            Color endMarker = Color.Green;
            //Stores the current position in the arrary...
            int currentPosition = 0;
            //Auxiliary object to be able to swap positions in the array....
            ColorChip temp;            

            //0(n) algorithm that sorts chips, serach for adjacent chips, and ends when the correct green chip is found....
            for (int i = 0; i < this._colorChips.Length;)
            {
                if (this._colorChips[i].StartColor == currentStart)//Validates that we have a correct start color...
                {
                    if (this._colorChips[i].EndColor == endMarker)//At this point a successfull link has been found from blue to green, so swaping and finishing the loop...
                    {
                        temp = this._colorChips[currentPosition];
                        this._colorChips[currentPosition] = this._colorChips[i];
                        this._colorChips[i] = temp;
                        canUnlock = true;
                        break;
                    }

                    temp = this._colorChips[currentPosition];

                    //Place item in the min position
                    this._colorChips[currentPosition] = this._colorChips[i];
                    this._colorChips[i] = temp;

                    currentStart = this._colorChips[currentPosition].EndColor;

                    currentPosition++;
                    i = currentPosition;
                }
                else //No matching start color found, continue to next chip....
                {
                    i++;
                }
            }

            if (canUnlock)
            {
                Console.WriteLine("Master panel unlocked:");
                Console.WriteLine(" ");
                for (int i = 0; i < currentPosition + 1; i++)
                {
                    Console.WriteLine($"[{this._colorChips[i].StartColor},{this._colorChips[i].EndColor}]");
                }

                if (currentPosition + 1 < this._colorChips.Length)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("Unused chips:");
                    Console.WriteLine(" ");
                    for (int i = currentPosition + 1; i < this._colorChips.Length; i++)
                    {
                        Console.WriteLine($"[{this._colorChips[i].StartColor},{this._colorChips[i].EndColor}]");
                    }
                }
            }
            else { Console.WriteLine(Constants.ErrorMessage); };

            Console.ReadLine();
        }
    }
}
