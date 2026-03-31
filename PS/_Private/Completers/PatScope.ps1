# PAT Scope

Register-ArgumentCompleter -ParameterName Scope -Verbose -ScriptBlock {
    
    param($commandName, $parameterName, $wordToComplete, $commandAst, $fakeBoundParameter)

    if ($commandName -notlike '*-TfsPersonalAccessToken') {
        return
    }

    $scopes = @(
        @{ Name = 'app_token';                             Description = 'Full access' }
        @{ Name = 'vso.advsec';                            Description = 'Advanced security (read)' }
        @{ Name = 'vso.advsec_write';                      Description = 'Advanced security (read and write)' }
        @{ Name = 'vso.advsec_manage';                     Description = 'Advanced security (read, write, and manage)' }
        @{ Name = 'vso.agentpools';                        Description = 'Agent pools (read)' }
        @{ Name = 'vso.agentpools_manage';                 Description = 'Agent pools (read and manage)' }
        @{ Name = 'vso.analytics';                         Description = 'Analytics (read)' }
        @{ Name = 'vso.auditlog';                          Description = 'Audit log (read)' }
        @{ Name = 'vso.auditstreams_manage';               Description = 'Audit streams (read)' }
        @{ Name = 'vso.build';                             Description = 'Build (read)' }
        @{ Name = 'vso.build_execute';                     Description = 'Build (read and execute)' }
        @{ Name = 'vso.code';                              Description = 'Code (read)' }
        @{ Name = 'vso.code_write';                        Description = 'Code (read and write)' }
        @{ Name = 'vso.code_manage';                       Description = 'Code (read, write, and manage)' }
        @{ Name = 'vso.code_full';                         Description = 'Code (full)' }
        @{ Name = 'vso.code_status';                       Description = 'Code (status)' }
        @{ Name = 'vso.connected_server';                  Description = 'Connected server' }
        @{ Name = 'vso.dashboards';                        Description = 'Team dashboards (read)' }
        @{ Name = 'vso.dashboards_manage';                 Description = 'Team dashboards (manage)' }
        @{ Name = 'vso.entitlements';                      Description = 'Entitlements (read)' }
        @{ Name = 'vso.environment_manage';                Description = 'Environment (read and manage)' }
        @{ Name = 'vso.extension';                         Description = 'Extensions (read)' }
        @{ Name = 'vso.extension_manage';                  Description = 'Extensions (read and manage)' }
        @{ Name = 'vso.extension.data';                    Description = 'Extension data (read)' }
        @{ Name = 'vso.extension.data_write';              Description = 'Extension data (read and write)' }
        @{ Name = 'vso.gallery';                           Description = 'Marketplace' }
        @{ Name = 'vso.gallery_acquire';                   Description = 'Marketplace (acquire)' }
        @{ Name = 'vso.gallery_publish';                   Description = 'Marketplace (publish)' }
        @{ Name = 'vso.gallery_manage';                    Description = 'Marketplace (manage)' }
        @{ Name = 'vso.githubconnections';                 Description = 'GitHub connections (read)' }
        @{ Name = 'vso.githubconnections_manage';          Description = 'GitHub connections (read and manage)' }
        @{ Name = 'vso.graph';                             Description = 'Graph (read)' }
        @{ Name = 'vso.graph_manage';                      Description = 'Graph (manage)' }
        @{ Name = 'vso.hooks';                             Description = 'Service hooks (read)' }
        @{ Name = 'vso.hooks_write';                       Description = 'Service hooks (read and write)' }
        @{ Name = 'vso.hooks_interact';                    Description = 'Service hooks (interact)' }
        @{ Name = 'vso.identity';                          Description = 'Identity (read)' }
        @{ Name = 'vso.identity_manage';                   Description = 'Identity (manage)' }
        @{ Name = 'vso.loadtest';                          Description = 'Load test (read)' }
        @{ Name = 'vso.loadtest_write';                    Description = 'Load test (read and write)' }
        @{ Name = 'vso.machinegroup_manage';               Description = 'Deployment group (read, manage)' }
        @{ Name = 'vso.memberentitlementmanagement';       Description = 'Member entitlement management (read)' }
        @{ Name = 'vso.memberentitlementmanagement_write'; Description = 'Member entitlement management (write)' }
        @{ Name = 'vso.notification';                      Description = 'Notifications (read)' }
        @{ Name = 'vso.notification_write';                Description = 'Notifications (write)' }
        @{ Name = 'vso.notification_manage';               Description = 'Notifications (manage)' }
        @{ Name = 'vso.notification_diagnostics';          Description = 'Notifications (diagnostics)' }
        @{ Name = 'vso.packaging';                         Description = 'Packaging (read)' }
        @{ Name = 'vso.packaging_write';                   Description = 'Packaging (read and write)' }
        @{ Name = 'vso.packaging_manage';                  Description = 'Packaging (read, write, and manage)' }
        @{ Name = 'vso.pipelineresources_use';             Description = 'Pipeline resources (use)' }
        @{ Name = 'vso.pipelineresources_manage';          Description = 'Pipeline resources (use and manage)' }
        @{ Name = 'vso.profile';                           Description = 'User profile (read)' }
        @{ Name = 'vso.profile_write';                     Description = 'User profile (write)' }
        @{ Name = 'vso.project';                           Description = 'Project and team (read)' }
        @{ Name = 'vso.project_write';                     Description = 'Project and team (read and write)' }
        @{ Name = 'vso.project_manage';                    Description = 'Project and team (read, write, and manage)' }
        @{ Name = 'vso.release';                           Description = 'Release (read)' }
        @{ Name = 'vso.release_execute';                   Description = 'Release (read, write, and execute)' }
        @{ Name = 'vso.release_manage';                    Description = 'Release (read, write, execute, and manage)' }
        @{ Name = 'vso.securefiles_read';                  Description = 'Secure files (read)' }
        @{ Name = 'vso.securefiles_write';                 Description = 'Secure files (read and create)' }
        @{ Name = 'vso.securefiles_manage';                Description = 'Secure files (read, create, and manage)' }
        @{ Name = 'vso.security_manage';                   Description = 'Security (manage)' }
        @{ Name = 'vso.serviceendpoint';                   Description = 'Service endpoints (read)' }
        @{ Name = 'vso.serviceendpoint_query';             Description = 'Service endpoints (read and query)' }
        @{ Name = 'vso.serviceendpoint_manage';            Description = 'Service endpoints (read, query, and manage)' }
        @{ Name = 'vso.settings';                          Description = 'Settings (read)' }
        @{ Name = 'vso.settings_write';                    Description = 'Settings (read and write)' }
        @{ Name = 'vso.symbols';                           Description = 'Symbols (read)' }
        @{ Name = 'vso.symbols_write';                     Description = 'Symbols (read and write)' }
        @{ Name = 'vso.symbols_manage';                    Description = 'Symbols (read, write, and manage)' }
        @{ Name = 'vso.taskgroups_read';                   Description = 'Task groups (read)' }
        @{ Name = 'vso.taskgroups_write';                  Description = 'Task groups (read and create)' }
        @{ Name = 'vso.taskgroups_manage';                 Description = 'Task groups (read, create, and manage)' }
        @{ Name = 'vso.test';                              Description = 'Test management (read)' }
        @{ Name = 'vso.test_write';                        Description = 'Test management (read and write)' }
        @{ Name = 'vso.threads_full';                      Description = 'PR threads' }
        @{ Name = 'vso.tokenadministration';               Description = 'Token administration' }
        @{ Name = 'vso.tokens';                            Description = 'Delegated authorization tokens' }
        @{ Name = 'vso.variablegroups_read';               Description = 'Variable groups (read)' }
        @{ Name = 'vso.variablegroups_write';              Description = 'Variable groups (read and create)' }
        @{ Name = 'vso.variablegroups_manage';             Description = 'Variable groups (read, create, and manage)' }
        @{ Name = 'vso.wiki';                              Description = 'Wiki (read)' }
        @{ Name = 'vso.wiki_write';                        Description = 'Wiki (read and write)' }
        @{ Name = 'vso.work';                              Description = 'Work items (read)' }
        @{ Name = 'vso.work_write';                        Description = 'Work items (read and write)' }
        @{ Name = 'vso.work_full';                         Description = 'Work items (full)' }
    )

    $scopes | Where-Object { $_.Name -like "$wordToComplete*" } | ForEach-Object {
        [System.Management.Automation.CompletionResult]::new(
            $_.Name,
            $_.Name,
            'ParameterValue',
            $_.Description
        )
    }
}
