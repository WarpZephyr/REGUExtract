# REGUExtract
Extracts, or really, unwraps regulation.bin from REGU.DAT in Armored Core Verdict Day save files.  
The extracted file is named regulation.bnd so Yabber and other related programs can more easily find it.  

To unpack, drag and drop REGU.DAT onto the exe  
To repack, drag and drop regulation.bnd onto the exe  

regulation.bnd must not be more than 256,000 bytes maximum  

# Working with regulation.bnd
After you have it unpacked you should use [Yabber][0] to unpack and repack regulation.bnd  
As for editing the params inside, for now you should use [ACFAParamEditor][1], replace the defs in the res folder of the editor with ACVD defs  

To get ACVD defs use [DVDUnbinder][2] to extract the bhd and bnd files in the game,  
Then go to boot.bnd in the bind folder,  
Then extract that with Yabber,  
Go to param/def/ to get all the defs for ACVD  

After you have the defs place them in ACFAParamEditor's res/def/ folder.  
It doesn't really support switching game defs, so just backup the ACFA defs there.  

Some files inside are Dbp params, in which case use [ParamDbpEditor][3] on them.  
Make sure to set the dbps being used to ACVD in the bottom left corner.  

The only other file is acvparts, this needs reverse engineering work, but contains part stats and is very important.

If you want to translate the ACVD defs since they are translated yet use [ParamDefEditor][4],  
There you should be able to dump them and repack them again.  

[0]: https://www.github.com/JkAnderson/Yabber/
[1]: https://www.github.com/WarpZephyr/ACFAParamEditor/
[2]: https://github.com/WarpZephyr/DVDUnbinder
[3]: https://www.github.com/WarpZephyr/ParamDbpEditor/
[4]: https://www.github.com/WarpZephyr/ParamDefEditor/