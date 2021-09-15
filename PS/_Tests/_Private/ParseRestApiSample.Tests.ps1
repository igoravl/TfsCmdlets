. "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\..\_TestSetup.ps1"

InModuleScope 'TfsCmdlets' {

    Describe '_ParseRestApiSample' {

        Context 'When supplying an invalid parameter' {

            $expected = [Ordered] @{ Operation = ''; Host = ''; Path = ''; QueryParameters = @{}; ApiVersion = ''; Scope = '' }

            It 'Should be empty on invalid data' {

                $samples = @(
                    'FOO https://dev.azure.com/{organization}/{project}/{team}/_apis/wit/wiql?api-version=5.1',
                    'GET https://foo/{organization}/{project}/{team}/_apis/wit/wiql?api-version=5.1'
                    'http://foo/bar/baz'
                    )
                
                    foreach($actual in $samples)
                    { 
                        _ParseRestApiSample $actual | ConvertTo-Json -Compress  | Should Be ($expected | ConvertTo-Json -Compress )
                    }
            }
        }

        Context 'When supplying a URL' {

            It 'Should return Host, Path, Query Params, API Version' {
                
                $sample = 'https://dev.azure.com/{organization}/{project}/{team}/_apis/wit/wiql?api-version=5.1'
                $expected = [Ordered] @{ Operation=''; Host = 'dev.azure.com'; Path='/{project}/{team}/_apis/wit/wiql'; QueryParameters = @{}; ApiVersion='5.1'; Scope = 'Organization'}
                (_ParseRestApiSample -Sample $sample) | ConvertTo-Json -Compress  | Should Be ($expected | ConvertTo-Json -Compress )

                $sample = 'https://app.vssps.visualstudio.com/_apis/accounts?api-version=5.1'
                $expected = [Ordered] @{ Operation=''; Host = 'app.vssps.visualstudio.com'; Path='/_apis/accounts'; QueryParameters = @{}; ApiVersion='5.1'; Scope = 'Service'}
                (_ParseRestApiSample -Sample $sample) | ConvertTo-Json -Compress  | Should Be ($expected | ConvertTo-Json -Compress )
            }
        }

        Context 'When supplying a regular REST API code sample' {

            It 'Should return Operation, Host, Path, Query Params, API Version' {
                
                $sample = 'POST https://dev.azure.com/{organization}/{project}/{team}/_apis/wit/wiql?api-version=5.1'
                $expected = [Ordered] @{ Operation='POST'; Host = 'dev.azure.com'; Path='/{project}/{team}/_apis/wit/wiql'; QueryParameters = @{}; ApiVersion='5.1'; Scope = 'Organization'}
                (_ParseRestApiSample -Sample $sample) | ConvertTo-Json -Compress  | Should Be ($expected | ConvertTo-Json -Compress )

                $sample = 'GET https://vsrm.dev.azure.com/{organization}/{project}/_apis/release/definitions?api-version=5.1'
                $expected = [Ordered] @{ Operation='GET'; Host="vsrm.dev.azure.com"; Path='/{project}/_apis/release/definitions'; QueryParameters = @{}; ApiVersion='5.1'; Scope = 'Organization'}
                (_ParseRestApiSample -Sample $sample) | ConvertTo-Json -Compress  | Should Be ($expected | ConvertTo-Json -Compress )

                $sample = 'GET https://app.vssps.visualstudio.com/_apis/accounts?api-version=5.1'
                $expected = [Ordered] @{ Operation='GET'; Host = 'app.vssps.visualstudio.com'; Path='/_apis/accounts'; QueryParameters = @{}; ApiVersion='5.1'; Scope = 'Service'}
                (_ParseRestApiSample -Sample $sample) | ConvertTo-Json -Compress  | Should Be ($expected | ConvertTo-Json -Compress )
            }
        }

        Context 'When supplying a regular REST API code sample with optional parameters' {

            It 'Should return Operation, Path, API Version' {

                $sample = 'POST https://dev.azure.com/{organization}/{project}/{team}/_apis/wit/wiql?timePrecision={timePrecision}&$top={$top}&api-version=5.1'
                $expected = [Ordered] @{ Operation='POST'; Host='dev.azure.com'; Path='/{project}/{team}/_apis/wit/wiql'; QueryParameters = @{}; ApiVersion='5.1'; Scope = 'Organization'}
                (_ParseRestApiSample -Sample $sample) | ConvertTo-Json -Compress  | Should Be ($expected | ConvertTo-Json -Compress )

                $sample = 'GET https://vsrm.dev.azure.com/{organization}/{project}/_apis/release/definitions?searchText={searchText}&$expand={$expand}&artifactType={artifactType}&artifactSourceId={artifactSourceId}&$top={$top}&continuationToken={continuationToken}&queryOrder={queryOrder}&path={path}&isExactNameMatch={isExactNameMatch}&tagFilter={tagFilter}&propertyFilters={propertyFilters}&definitionIdFilter={definitionIdFilter}&isDeleted={isDeleted}&searchTextContainsFolderName={searchTextContainsFolderName}&api-version=5.1'
                $expected = [Ordered] @{ Operation='GET'; Host = 'vsrm.dev.azure.com'; Path='/{project}/_apis/release/definitions'; QueryParameters = @{}; ApiVersion='5.1'; Scope = 'Organization'}
                (_ParseRestApiSample -Sample $sample) | ConvertTo-Json -Compress  | Should Be ($expected | ConvertTo-Json -Compress )

                $sample = 'GET https://app.vssps.visualstudio.com/_apis/accounts?ownerId={ownerId}&memberId={memberId}&properties={properties}&api-version=5.1'
                $expected = [Ordered] @{ Operation='GET'; Host = 'app.vssps.visualstudio.com'; Path='/_apis/accounts'; QueryParameters = @{}; ApiVersion='5.1'; Scope = 'Service'}
                (_ParseRestApiSample -Sample $sample) | ConvertTo-Json -Compress  | Should Be ($expected | ConvertTo-Json -Compress )
            }
        }

    }

}