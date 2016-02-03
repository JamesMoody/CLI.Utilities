Imports System
Imports System.Deployment
Imports System.Configuration
Imports CLI.Utilities


Module Module1

    Sub Main()

        Dim exe As String = System.Reflection.Assembly.GetExecutingAssembly().Location
        Dim item As New EncryptConfig(exe)
        Console.WriteLine("protected? {0}", item.EncryptConnectionStrings())
        Console.WriteLine("value: {0}", ConfigurationManager.ConnectionStrings("testItem").ConnectionString)

        CommandLine.PressAnyKey()
    End Sub

End Module
