using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameDataObjects
{
    public class Game
    {
        /// <summary>
        /// Database Id of the game.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the game.
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Console that the relevant version of the game can be found on.
        /// </summary>
        [Required]
        public string Console { get; set; }
        /// <summary>
        /// Name of the developer of the game.
        /// </summary>
        public string Developer { get; set; }
        /// <summary>
        /// Name of the publisher of the game.
        /// </summary>
        public string Publisher { get; set; }
        /// <summary>
        /// String representation of the user tied to this game;
        /// </summary>
        public bool IsOwned { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || (obj.GetType() != typeof(Game)))
            {
                return false;
            }
            Game other = obj as Game;
            return this.Id == other.Id && this.Console == other.Console && this.Developer == other.Developer && this.Name == other.Name &&
                   this.Publisher == other.Publisher;
        }

        /// <summary>
        /// Credit to the first answer:
        /// https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode
        /// which this is a near verbatim copy of. This was made primarily to remove the warning.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked 
            {
                int hash = 17;
                //I wish I could use the ?. operators, but they don't work in Visual Studio 2013, so...
                hash = hash * 23 + Id.GetHashCode();
                hash = hash * 23 + Console.GetHashCode();
                if (Developer != null)
                {
                    hash = hash * 23 + Developer.GetHashCode();
                }
                if (Publisher != null)
                {
                    hash = hash * 23 + Publisher.GetHashCode();
                }
                hash = hash * 23 + Name.GetHashCode();
                
                return hash;
            }
        }

        public static bool operator ==(Game g1, Game g2)
        {
            //if g1 is null...
            if ((object)g1 == null)
            {
                //check if g2 is null. If so, they are equal! If not, then not.
                if ((object)g2 == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //Otherwise, just call equals.
            else
            {
                return g1.Equals(g2);
            }
        }

        public static bool operator !=(Game g1, Game g2)
        {
            return !(g1 == g2);
        }
    }
}
