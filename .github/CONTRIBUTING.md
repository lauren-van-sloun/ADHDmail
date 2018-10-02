## <a id="how-to-contribute">How to contribute?</a>
Your contributions to ADHDmail are very welcome.
If you find a bug, please raise it as an issue.
Even better fix it and send a pull request.
If you like to help out with existing bugs and feature requests just check out the list of [issues](https://github.com/Ashera138/ADHDmail/issues) and grab and fix one.
Some of the issues are labeled as as `jump in`. These issues are generally low hanging fruit so you can start with easier tasks.

This project has adopted the code of conduct defined by the [Contributor Covenant](http://contributor-covenant.org/)
to clarify expected behavior in our community. 
For more information see the [.NET Foundation Code of Conduct](http://www.dotnetfoundation.org/code-of-conduct).

### <a id="getting-started">Getting started</a>
This project uses C# 7 language features and SDK-style projects, so you'll need any edition of [Visual Studio 2017](https://www.visualstudio.com/downloads/download-visual-studio-vs) to open and compile the project. The free [Community Edition](https://go.microsoft.com/fwlink/?LinkId=532606&clcid=0x409) will work.

### <a id="contribution-guideline">Contribution guideline</a>
This project uses [GitHub flow](http://scottchacon.com/2011/08/31/github-flow.html) for pull requests.
So if you want to contribute, fork the repo, preferably create a local branch, based off of the `dev` branch to avoid conflicts with other activities, fix an issue, and send a PR.
If you are new to git, [here is a guide](https://help.github.com/articles/fork-a-repo/) to forking and cloning a repository.

Pull requests are code reviewed. Here is a checklist you should tick through before submitting a pull request:

 - Here is a checklist you should tick through before submitting a pull request: 
 - Implementation is clean
 - Code adheres to the existing coding standards; e.g. no curlies for one-line blocks, no redundant empty lines between methods or code blocks, spaces rather than tabs, etc. Full conventions can be found here: (add link to coding-style.md)
 - There is proper unit test coverage
 - If the code is copied from StackOverflow (or a blog or OSS) full disclosure is included. That includes required license files and/or file headers explaining where the code came from with proper attribution
 - There are very few or no comments (because comments shouldn't be needed if you write clean code)
 - XML documentation is added/updated for the addition/change
 - Your PR is (re)based on top of the latest commits from the `dev` branch (more info below)
 - Link to the issue(s) you're fixing from your PR description. Use `fixes #<the issue number>`
 - Readme is updated if you change an existing feature or add a new one

Please rebase your code on top of the latest `dev` branch commits.
Before working on your fork make sure you pull the latest so you work on top of the latest commits to avoid merge conflicts.
Also before sending the pull request please rebase your code as there is a chance there have been new commits pushed after you pulled last.
Please refer to [this guide](https://gist.github.com/jbenet/ee6c9ac48068889b0912#the-workflow) if you're new to git.

<hr>
**Working on your first Pull Request?** You can learn how from this *free* series [How to Contribute to an Open Source Project on GitHub](https://egghead.io/series/how-to-contribute-to-an-open-source-project-on-github)
<hr>

Source for most of this text: https://github.com/Humanizr/Humanizer/blob/master/CONTRIBUTING.md
