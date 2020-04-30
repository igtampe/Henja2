Public Class ABEditor
    Implements IHenjaEditorMode

    Public Sub KeyPress(Key As ConsoleKeyInfo) Implements IHenjaEditorMode.KeyPress
        Throw New NotImplementedException()
    End Sub

    Public Sub Render() Implements IHenjaEditorMode.Render
        Throw New NotImplementedException()
    End Sub

    Public Function GetName() As String Implements IHenjaEditorMode.GetName
        Return "ABScript Editor"
    End Function

    Public Function GenerateNew(Width As Integer, Height As Integer) As String() Implements IHenjaEditorMode.GenerateNew

        Return {""}
    End Function

End Class
