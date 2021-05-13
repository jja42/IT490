#!/bin/bash
find .. -maxdepth 1 -type f -printf '%f' | xargs -0 -I{} rm '/var/www/IT490Website/pages/{}'
