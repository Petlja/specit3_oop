public void Test()
{
    im = new Image();
    int w = ClientSize.Width;
    int h = ClientSize.Height;
    int pauseDuration = 1000;
    Layer l1 = new Layer(w, h, true, Color.Green);
    Layer l2 = new Layer(w, h, false, Color.Purple);
    im.AddLayer(l1);
    im.AddLayer(l2);

    Rectangle r11 = new Rectangle(10, 10, 160, 120, Color.Red, true);
    Rectangle r12 = new Rectangle(20, 20, 160, 120, Color.Blue, true);
    Rectangle r13 = new Rectangle(30, 30, 160, 120, Color.White, false);
    l1.Add(r11); l1.Add(r12); l1.Add(r13);

    Rectangle r21 = new Rectangle(60, 60, 40, 30, Color.Cyan, true);
    Rectangle r22 = new Rectangle(70, 70, 40, 30, Color.Yellow, true);
    l2.Add(r21); l2.Add(r22);
    Refresh(); // a
    Thread.Sleep(pauseDuration);

    l1.Select(r12);
    l1.BringToFront();
    Refresh(); // b
    Thread.Sleep(pauseDuration);

    l1.SetToBack();
    Refresh(); // c
    Thread.Sleep(pauseDuration);

    im.SelectLayer(0);
    im.BringToFront();
    Refresh(); // d
    Thread.Sleep(pauseDuration);

    im.SelectLayer(1);
    im.SetToBack();
    Refresh(); // e
    Thread.Sleep(pauseDuration);

    l2.SetBackground(true);
    Refresh(); // f
}
