namespace calc;

internal record struct FilePos(string File, int Line, int Column)
{
    public FilePos(int line, int column) : this("", line, column) { }
    public bool IsValid => File is not null && Line > 0 && Column > 0;
    public override string ToString() => IsValid ? $"{File}:{Line}:{Column}: " : "";
}
