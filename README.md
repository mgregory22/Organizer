# Organizer

A command-line organizer:
    Task list

Something I hack on once in awhile; nothing serious.  Primarily an exploration
in C#, program design and TDD.

Requires my MSG library (https://github.com/mgregory22/MSG.git), which is really
part of the development of this organizer, but eventually it will be useful on
its own.

I haven't figured out how to conveniently organize the projects and solution
file in GitHub, so the following flaws exist:
  * There's a solution file in this project that references the MSG and MSGTest
    projects with the hardcoded relative directories ../Lib/MSG/MSG and
    ../Lib/MSG/MSGTest
  * I sync the two repos by putting the commit hash of the MSG repo in the
    commit message of this repo, but I'm trying to always update the two repos
    at the same time so the latest of each will always be in sync.

