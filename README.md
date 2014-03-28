TypeScript Fast Build
===================

Visual studio build tasks to compile typescript files only when needed


#Why?

The default visual studio Typescript .targets call tsc.exe compiler every time you build a project, even when you didn't change any typescript file.

A simple solution is extend Microsoft.TypeScript.targets to call tsc.exe compiler only when needed.
