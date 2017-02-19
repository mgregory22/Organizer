#!/usr/bin/python

import os
import re

comment_re = re.compile('// (\w+\/)+\w+\.cs\n')

# traverse root directory, and list directories as dirs and files as files
for root, dirs, files in os.walk('.'):
    path = root.split(os.sep)[1:]
    #print((len(path) - 1) * '---', os.path.basename(root))
    if not 'obj' in path:
        for file in files:
            if file.endswith('.cs') and file != 'AssemblyInfo.cs':
                # generate file name and print it
                filename = '/'.join(path) + '/' + file
                print(filename)

                # read in the file lines
                with open(filename, encoding='utf-8-sig') as f:
                    lines = f.readlines()

                # if there's not already a filename comment at the beginning
                if not (lines[0] == "//\n" and comment_re.match(lines[1]) and lines[2] == "//\n"):
                    # insert 3 comment lines into the file
                    lines.insert(0, [ "//\n", "", "//\n" ])

                # make sure there is a blank line after the comment
                if not lines[3] == "\n":
                    lines.insert(3, "\n")

                # update the filename comment
                lines[0] = "//\n"
                lines[1] = '// ' + filename + "\n"
                lines[2] = "//\n"

                # find and update the namespace line
                for i in range(len(lines)):
                    if lines[i].startswith('namespace'):
                        lines[i] = 'namespace ' + '.'.join(path) + "\n"
                        break

                with open(filename, mode='w', encoding='utf-8-sig', newline="\r\n") as f:
                    f.writelines(lines)
