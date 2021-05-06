using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARPGGamepadWinForm
{
    public partial class About : Form
    {
        private readonly string AboutText;

        public About()
        {
            InitializeComponent();

            AboutText = @"ARPG Gamepad Controller
Version 2.1
Developed by Roberto Julián Rodríguez Tapia, part of Cute Kick Studio.
For help on how to use it check the Readme.txt file

Check us at www.cutekickstudio.com

I enjoy playing ARPG games (Diablo2, Diablo3, Titan Quest, Path of Exile, etc), after having some Carpal Tunnel problems, I had to stop playing exclusively with the mouse, rather than stop playing, I started looking at apps that would help me translate a gamepad to mouse & keyboard.

No app was comfortable to use, specially for movement, until I found a simple app made for Diablo 3, this app however, lacked a way to aim properly and after it stopped being updated, I decided it was time to make my own and thus, created this app.
I've had a lot of fun working on and using this app and I plan to continue my work on it.


This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.
";

            txtAbout.Text = AboutText;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
