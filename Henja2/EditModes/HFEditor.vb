Public Class HFEditor
    Implements IHenjaEditorMode

    Public Sub New()

    End Sub

    Public Sub KeyInput(CurrentKey As ConsoleKeyInfo) Implements IHenjaEditorMode.KeyPress



    End Sub


    Public Sub Render() Implements IHenjaEditorMode.Render
        Dim X As Integer = 1

        For Each Line As String In Currentdocument
            SetPos(0, X)
            HiColorDraw(Line)
            X += 1
        Next
    End Sub

    Public Function GetName() As String Implements IHenjaEditorMode.GetName
        Return "HiColor Editor"
    End Function

    Public Function GenerateNew(Width As Integer, Height As Integer) As String() Implements IHenjaEditorMode.GenerateNew

        Dim ReturnArray(Height - 1) As String

        For Y = 0 To Height - 1
            ReturnArray(Y) = ""
            For X = 0 To Width
                ReturnArray(Y) &= "FF0-"
            Next
            ReturnArray(Y) = ReturnArray(Y).TrimEnd("-")
        Next

        Return ReturnArray
    End Function
End Class
