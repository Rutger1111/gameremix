using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace GameRemixForLinux
{
    internal partial class RenderForm : Form
    {
        private float unit = 0;
        private readonly float factor = 1080.0f / 1920.0f;
        private int heightPadding;

        private const int tileSize = 16;

        private readonly Image image;

        private readonly LevelLoader levelLoader = new LevelLoader(tileSize, new FileLevelDataSource());
        private readonly RenderObject player = new RenderObject();

        private Room room;
        private readonly Dictionary<char, Rectangle> tileMap = new Dictionary<char, Rectangle>();
        private float frametime;
        private bool goal;

        internal RenderForm()
        {
            InitializeComponent();

            image = Bitmap.FromFile("sprites.png");

            tileMap.Add('#', new Rectangle(45, 75, 16, 16));
            tileMap.Add('.', new Rectangle(23, 75, 16, 16));
            tileMap.Add('D', new Rectangle(2, 75, 16, 16));
            tileMap.Add('!', new Rectangle(66, 75, 16, 16));

            levelLoader.LoadRooms(tileMap);
            room = levelLoader.GetRoom(0, 0);

            player = new RenderObject()
            {
                rectangle = new Rectangle(2 * tileSize, 2 * tileSize, tileSize, tileSize),
                frames = new Rectangle[]
                {
                    new Rectangle(43, 9, 16, 16),
                    new Rectangle(60, 9, 16, 16),
                    new Rectangle(77, 9, 16, 16)
                }
            };

            DoubleBuffered = true;
            ResizeRedraw = true;

            KeyDown += RenderForm_KeyDown;
            FormClosing += Form1_FormClosing;
            Resize += RenderForm_Resize;
            SizeChanged += RenderForm_SizeChanged;
            Load += RenderForm_Load;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            image.Dispose();
        }
        private void RenderForm_Load(object sender, EventArgs e)
        {
            heightPadding = ClientSize.Height - Height;
            RenderForm_SizeChanged(null, null);
        }
        private void RenderForm_Resize(object sender, EventArgs e)
        {
            RenderForm_SizeChanged(null, null);
        }

        private void RenderForm_SizeChanged(object sender, EventArgs e)
        {

            unit = ClientSize.Width / (float)(20 * tileSize);
            int height = (int)(ClientSize.Width * factor);
            if (ClientSize.Height != 0)
            {
                Height = heightPadding + height;
            }
        }
        private void RenderForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                MovePlayer(0, -1);
            }
            else if (e.KeyCode == Keys.S)
            {
                MovePlayer(0, 1);
            }
            else if (e.KeyCode == Keys.A)
            {
                MovePlayer(-1, 0);
            }
            else if (e.KeyCode == Keys.D)
            {
                MovePlayer(1, 0);
            }
        }

        private void MovePlayer(int x, int y)
        {
            if (goal)
            {
                return;
            }
            float newx = player.rectangle.X + (x * tileSize);
            float newy = player.rectangle.Y + (y * tileSize);

            Tile next = room.tiles.SelectMany(ty => ty.Where(tx => tx.rectangle.Contains((int)newx, (int)newy))).FirstOrDefault();

            if (next == null)
            {

            }
            else
            {
                if (next.graphic == 'D')
                {
                    room = levelLoader.GetRoom(room.roomx + x, room.roomy + y);

                    if (y != 0)
                    {
                        player.rectangle.Y += -y * ((room.tiles.Length - 2) * tileSize);
                    }
                    else
                    {
                        player.rectangle.X += -x * ((room.tiles[0].Length - 2) * tileSize);
                    }
                }

                else if (next.graphic == '!')
                {
                    player.rectangle.X = newx;
                    player.rectangle.Y = newy;
                    goal = true;

                }
                else if (next.graphic != '#')
                {
                    player.rectangle.X = newx;
                    player.rectangle.Y = newy;
                }
            }
        }

        internal void Logic(float frametime)
        {
            this.frametime = frametime;
        }
        private Graphics InitGraphics(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.None;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.Transform = new Matrix();
            g.ScaleTransform(unit, unit);

            g.Clear(Color.Black);
            return g;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = InitGraphics(e);
            foreach (Tile[] row in room.tiles)
            {
                foreach (Tile t in row)
                {
                    g.DrawImage(image, t.rectangle, t.sprite, GraphicsUnit.Pixel);
                }
            }
            if (goal)
            {
                g.DrawString("GOAL", Font, Brushes.Green, 50, 50);
            }
            else
            {

                g.DrawImage(image, player.rectangle, player.frames[(int)player.frame], GraphicsUnit.Pixel);
                MoveFrame(player);
            }
        }

        private void MoveFrame(RenderObject item)
        {
            item.frame += frametime * item.animationSpeed;
            if (item.frame >= item.frames.Length)
            {
                item.frame = 0;
            }
        }

    }

}



