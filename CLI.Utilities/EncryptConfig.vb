Imports System.Configuration

Public Class EncryptConfig

#Region " init (overloaded) "

    Sub New()
        ' no-op
    End Sub

#End Region
#Region " init (overloaded) "
    Sub New(ByVal TargetExe As String)
        TargetExecutable = TargetExe
    End Sub

#End Region

#Region " properties - TargetExecutable "

    ''' <summary>
    ''' This is the executable file name without the .config extension.
    ''' </summary>
    ''' <returns></returns>
    Public Property TargetExecutable As String = String.Empty

#End Region

#Region " methods - EncryptConnectionStrings "

    Public Function EncryptConnectionStrings() As Boolean

        ' sanity check
        If TargetExecutable.Length = 0 Then
            Throw New InvalidOperationException("TargetExecutable is missing.")
        End If

        ' get the config file
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(TargetExecutable)

        ' open the target section
        Dim section As ConnectionStringsSection = DirectCast(config.GetSection("connectionStrings"), ConnectionStringsSection)

        ' if we have the section AND it's not already encrypted then...
        If section IsNot Nothing AndAlso Not section.SectionInformation.IsProtected Then
            ' encrypt & save
            section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider")
            config.Save()
        End If


        Return section IsNot Nothing AndAlso section.SectionInformation.IsProtected  ' when we're done, the section should be encrypted

    End Function

#End Region
#Region " methods - DecryptConnectionStrings "

    Public Function DecryptConnectionStrings() As Boolean

        ' sanity check
        If TargetExecutable.Length = 0 Then
            Throw New InvalidOperationException("TargetExecutable is missing.")
        End If

        ' get the config file
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(TargetExecutable)

        ' open the target section
        Dim section As ConnectionStringsSection = DirectCast(config.GetSection("connectionStrings"), ConnectionStringsSection)

        ' if we have the section AND it's already encrypted then...
        If section IsNot Nothing AndAlso section.SectionInformation.IsProtected Then
            ' remove the encryption & save
            section.SectionInformation.UnprotectSection()
            config.Save()
        End If


        Return section IsNot Nothing AndAlso section.SectionInformation.IsProtected ' when we're done, the section should not be encrypted

    End Function

#End Region

End Class
