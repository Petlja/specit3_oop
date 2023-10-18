public void Test()
{
    im = new Image();
    int w = ClientSize.Width;
    int h = ClientSize.Height;
    
    // pauza od 1 sekunde se koristi da bi slike mogle da se vide
    int pauseDuration = 1000; 
    
    // kreiramo dva nova sloja i dodajemo ih u sliku
    Layer l1 = new Layer(w, h, true, Color.Green);
    Layer l2 = new Layer(w, h, false, Color.Purple);
    im.AddLayer(l1);
    im.AddLayer(l2);

    // kreiramo tri nova pravougaonika i dodajemo ih u prvi sloj
    Rectangle r11 = new Rectangle(10, 10, 160, 120, Color.Red, true);
    Rectangle r12 = new Rectangle(20, 20, 160, 120, Color.Blue, true);
    Rectangle r13 = new Rectangle(30, 30, 160, 120, Color.White, false);
    l1.Add(r11); l1.Add(r12); l1.Add(r13);

    // kreiramo dva nova pravougaonika i dodajemo ih u drugi sloj
    Rectangle r21 = new Rectangle(60, 60, 40, 30, Color.Cyan, true);
    Rectangle r22 = new Rectangle(70, 70, 40, 30, Color.Yellow, true);
    l2.Add(r21); l2.Add(r22);

    Refresh(); // zahtevamo ponovno iscrtavanje slike (slika a)
    Thread.Sleep(pauseDuration);

    l1.Select(r12);
    l1.BringToFront();
    Refresh(); // zahtevamo ponovno iscrtavanje slike (slika b)
    Thread.Sleep(pauseDuration);

    l1.SetToBack();
    Refresh(); // zahtevamo ponovno iscrtavanje slike (slika c)
    Thread.Sleep(pauseDuration);

    im.SelectLayer(0);
    im.BringToFront();
    Refresh(); // zahtevamo ponovno iscrtavanje slike (slika d)
    Thread.Sleep(pauseDuration);

    im.SelectLayer(1);
    im.SetToBack();
    Refresh(); // zahtevamo ponovno iscrtavanje slike (slika e)
    Thread.Sleep(pauseDuration);

    l2.SetBackground(true);
    Refresh(); // zahtevamo ponovno iscrtavanje slike (slika f)
}
