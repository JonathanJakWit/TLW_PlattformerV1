using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLW_Plattformer.RipyGame.Models
{
    public class Clickable
    {
        protected Rectangle _bounds;
        protected Vector2 position;

        public bool IsClicked;

        public Clickable(Rectangle bounds, Vector2 position)
        {
            this._bounds = bounds;
            this.position = position;
            this.IsClicked = false;
        }
        public void OnClick()
        {
            if (IsClicked == false)
            {
                IsClicked = true;
            }
            else
            {
                IsClicked = false;
            }
        }

        public bool CheckIfInBounds(Point posToCheck)
        {
            if (_bounds.Contains(posToCheck))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
