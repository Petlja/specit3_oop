namespace DekorisaniOblici
{
    // klasa koja sadrži kolekciju (listu) sprajtova 
    // i upravlja svim sprajtovima
    class SpriteManager
    {
        List<Sprite> sprites = new List<Sprite>();
        
        public void AddSprite(Sprite s)
        {
            sprites.Add(s);
        }

        public void Update()
        {
            foreach (Sprite s in sprites)
                s.Update();
        }

        public void Draw(Graphics g)
        {
            foreach (Sprite s in sprites)
                s.Draw(g);
        }
    }
}
