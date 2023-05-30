namespace grupisanje
{
    class SpriteGroup : Sprite
    {
        private List<Sprite> sprites = new List<Sprite>();

        public void AddSprite(Sprite s) 
        {
            sprites.Add(s); 
        }
        public override float X { get { return MinX(); } }

        public override float Y { get { return MinY(); } }

        public override float Width { get { return MaxX() - MinX(); } }

        public override float Height { get { return MaxY() - MinY(); } }

        public override IEnumerable<Sprite> SubSprites()
        {
            foreach (Sprite s in sprites) 
                yield return s;
        }
        public override bool Contains(float x, float y)
        {
            foreach (Sprite s in sprites)
                if (s.Contains(x, y)) return true;

            return false;
        }
        public override void Move(float dx, float dy)
        {
            foreach (Sprite s in sprites)
                s.Move(dx, dy);
        }
        public override void Draw(Graphics g)
        {
            foreach (Sprite s in sprites)
                s.Draw(g);
        }
        private float MinX() { return sprites.Select(sprite => sprite.X).Min(); }
        private float MaxX() { return sprites.Select(sprite => sprite.X + sprite.Width).Max(); }
        private float MinY() { return sprites.Select(sprite => sprite.Y).Min(); }
        private float MaxY() { return sprites.Select(sprite => sprite.Y + sprite.Height).Max(); }
    }
}
