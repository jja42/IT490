#!/bin/bash
find $1 -maxdepth 1  -type f -printf '%f' | xargs -0 -I{} cp "$1{}" /var/www/IT490Website/pages
