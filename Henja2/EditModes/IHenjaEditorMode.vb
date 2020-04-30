Public Interface IHenjaEditorMode

    Sub KeyPress(Key As ConsoleKeyInfo)
    Sub Render()
    Function GetName() As String
    Function GenerateNew(X As Integer, Y As Integer) As String()

End Interface
