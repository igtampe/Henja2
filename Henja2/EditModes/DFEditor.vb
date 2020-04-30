Public Class DFEditor
    Implements IHenjaEditorMode

    Public Sub KeyPress(Key As ConsoleKeyInfo) Implements IHenjaEditorMode.KeyPress

        Select Case Key.Key
            Case ConsoleKey.D0, ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.D7, ConsoleKey.D8, ConsoleKey.D9, ConsoleKey.A, ConsoleKey.B, ConsoleKey.C, ConsoleKey.D, ConsoleKey.E, ConsoleKey.F
                Currentdocument(CurrentY) = Currentdocument(CurrentY).Remove(CurrentX, 1).Insert(CurrentX, Key.KeyChar.ToString.ToUpper)
                Draw(Key.KeyChar.ToString.ToUpper)
        End Select
    End Sub

    Public Sub Render() Implements IHenjaEditorMode.Render
        Dim X As Integer = 1

        For Each Line As String In Currentdocument
            SetPos(0, X)
            Draw(Line)
            X += 1
        Next

    End Sub

    Public Function GetName() As String Implements IHenjaEditorMode.GetName
        Return "DF Editor"
    End Function

    Public Function GenerateNew(Width As Integer, Height As Integer) As String() Implements IHenjaEditorMode.GenerateNew

        Dim ReturnArray(Height - 1) As String

        For Y = 0 To Height - 1
            ReturnArray(Y) = ""
            For X = 0 To Width
                ReturnArray(Y) &= "F"
            Next
        Next

        Return ReturnArray
    End Function

End Class
