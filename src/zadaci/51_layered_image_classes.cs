public class Rectangle
{
    public Rectangle(
        float x0, float y0, // polozaj
        float w0, float h0, // dimenzije
        Color c, // boja
        bool f // da li je ispunjen
        ) { }
    public void Draw(Graphics g) { } // iscrtava pravougaonik
}

public class Layer
{
    public Layer(
        int w0, int h0, // velicina
        bool opaque, // da li je pozadina neprovidna
        Color c // boja pozadine
        ) { }
    public void Add(Rectangle r) { } // dodaje pravougaonik u ovaj sloj
    public void SetBackground(bool opaque) { } // postavlja neprovidnost pozadine
    public void SetBackground(bool opaque, Color c) { } // postavlja neprovidnost i boju pozadine
    public bool Select(Rectangle r) { return true; } // selektuje pravougaonik (ako je u sloju)
    public bool BringToFront() { return true; } // pomera pravougaonik na vrh
    public bool SetToBack() { return true; } // pomera pravougaonik na dno
    public void Draw(Graphics g) { } // iscrtava sloj
}

public class Image
{
    public void SelectLayer(int i) { } // bira aktivan sloj (na koji se odnose komande pomeranja sloja)
    public void AddLayer(Layer l) { } // dodaje sloj
    public void BringToFront() { }  // pomera sloj na vrh
    public void SetToBack() { }     // pomera sloj na dno
    public void Draw(Graphics g) { } // iscrtava sliku
}
