Public Class HFEditor
    Implements IHenjaEditorMode

    Private myColorSelector As HiColorPicker
    Private SelectedColor As String

    Public Sub New()
        myColorSelector = New HiColorPicker
        SelectedColor = "000"


    End Sub

    Public Sub KeyInput(Key As ConsoleKeyInfo) Implements IHenjaEditorMode.KeyPress

        If Key.Key = ConsoleKey.C Then

            Dim NewColorString As String = myColorSelector.PickColor
            If Not IsNothing(NewColorString) Then SelectedColor = NewColorString

            Render()

        ElseIf Key.Key = ConsoleKey.Spacebar Then
            Dim currentLine() As String = Currentdocument(CurrentY).Split("-")
            currentLine(CurrentX) = SelectedColor
            Currentdocument(CurrentY) = String.Join("-", currentLine)

            HiColorDraw(SelectedColor)

        End If

    End Sub


    Public Sub Render() Implements IHenjaEditorMode.Render
        Dim X As Integer = 1

        Sprite("CTRL+S to save, Draw with Spacebar, choose color with C.", ConsoleColor.Black, ConsoleColor.White, 0, 0)

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
