RAW_API=gkeyfile-api.raw
API=gkeyfile-api.xml
METADATA=GKeyFile.metadata

ASSEMBLY_NAME= gkeyfile-sharp
ASSEMBLY= $(ASSEMBLY_NAME).dll

CODEGEN=gapi2-codegen
FIXUP=gapi2-fixup

GENERATED_SRCDIR = generated
GENERATED_SRCS = $(GENERATED_SRCDIR)/*.cs

customs = GKeyFile.custom

CLEANFILES = gkeyfile-sharp.dll $(GENERATED_SRCS) $(API) gkeyfile-sharp.snk

DISTCLEANFILES= AssemblyInfo.cs

noinst_DATA = gkeyfile-sharp.dll

moduledir = $(prefix)/lib/mono/gkeyfile-sharp/
module_SCRIPTS = gkeyfile-sharp.dll.config

all: gkeyfile-sharp.dll

$(API): $(RAW_API) $(METADATA)
	cp $< $@
	chmod u+w $@
	$(FIXUP) --api=$@ --metadata=$(lastword $^)

$(GENERATED_SRCS): $(API)
	 $(CODEGEN) --generate $< $(GLIB_SHARP_CFLAGS) --outdir=$(GENERATED_SRCDIR) --customdir=$(srcdir) --assembly-name=gkeyfile-sharp

gkeyfile-sharp.dll: AssemblyInfo.cs $(GENERATED_SRCS)
	$(CSC) -keyfile:$(top_srcdir)/gkeyfile-sharp.snk -out:$@ -unsafe -target:library $(GLIB_SHARP_LIBS) $^

install-data-local:
	echo "$(GACUTIL) /i $(ASSEMBLY) /f $(GACUTIL_FLAGS)";  \
        $(GACUTIL) /i $(ASSEMBLY) /f $(GACUTIL_FLAGS) || exit 1;

uninstall-local:
	echo "$(GACUTIL) /u $(ASSEMBLY_NAME) /package gkeyfile-sharp /root $(DESTDIR)$(libdir)"; \
        $(GACUTIL) /u $(ASSEMBLY_NAME) /package gkeyfile-sharp /root $(DESTDIR)$(libdir) || exit 1;

EXTRA_DIST = $(API) $(RAW_API) $(sources) $(customs) $(METADATA) gkeyfile-sharp.dll.config.in
