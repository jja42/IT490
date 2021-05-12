#!/bin/bash
find .. -maxdepth 1 -type f -printf '%f' | xargs -0 -I{} rm '../../pages/{}'
