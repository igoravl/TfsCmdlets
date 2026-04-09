# Using Get-TfsGitRepository with Recycle Bin

The `Get-TfsGitRepository` cmdlet now supports retrieving repositories from the Recycle Bin using the `-IncludeRecycleBin` parameter.

## Examples

### Get all repositories including those in the recycle bin
```powershell
Get-TfsGitRepository -Repository '*' -IncludeRecycleBin -Project 'MyProject'
```

### Find a specific deleted repository by name
```powershell
Get-TfsGitRepository -Repository 'DeletedRepo' -IncludeRecycleBin -Project 'MyProject'
```

### Find deleted repositories matching a pattern
```powershell
Get-TfsGitRepository -Repository 'Old*' -IncludeRecycleBin -Project 'MyProject'
```

### Find a deleted repository by ID
```powershell
Get-TfsGitRepository -Repository '12345678-1234-1234-1234-123456789012' -IncludeRecycleBin -Project 'MyProject'
```

## Output Types

When `-IncludeRecycleBin` is used, the cmdlet may return two types of objects:

- **GitRepository**: Active repositories
- **GitDeletedRepository**: Deleted repositories from the recycle bin

### GitDeletedRepository Properties

The `GitDeletedRepository` object includes the following properties:

- `Id`: The repository ID
- `Name`: The repository name
- `ProjectReference`: Reference to the team project
- `DeletedBy`: The user who deleted the repository
- `CreatedDate`: When the repository was originally created
- `DeletedDate`: When the repository was deleted

## API Reference

This feature uses the Azure DevOps REST API endpoint:
```
GET https://dev.azure.com/{organization}/{project}/_apis/git/recycleBin/repositories?api-version=7.1
```

For more information, see the [official Azure DevOps REST API documentation](https://learn.microsoft.com/en-us/rest/api/azure/devops/git/repositories/get-recycle-bin-repositories?view=azure-devops-rest-7.1).