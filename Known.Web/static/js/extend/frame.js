layui.define("common",function(n){function e(){return top.layui.admin.getCurTab()}function s(n){var f=n.name,i=n.config,s=n.toolbar||{},l=null,h={},c=f+"_key",v=this,a,r;t.extend(i,{elem:"#"+f,skin:"line",even:!0,page:!0,cellMinWidth:80});i.toolbar||(a=e(),r="<div>",a.module&&a.module.children&&a.module.children.forEach(function(n){r+='<button class="layui-btn layui-btn-sm" lay-event="'+n.code+'">';r+='<i class="layui-icon '+n.icon+'"><\/i><span>'+n.title+"<\/span>";r+="<\/button>"}),(i.url||n.showSearch)&&(r+='<span class="grid-search">',r+='  <input type="text" id="'+c+'" placeholder="请输入查询关键字" class="layui-input" autocomplete="off">',r+='  <i class="layui-icon layui-icon-search" lay-event="search"><\/i>',r+="<\/span>",s.search=function(){var n=t("#"+c).val();v.reload({key:n});t("#"+c).val(n)}),r+="<\/div>",i.toolbar=r);u.on("toolbar("+f+")",function(n){var t=n.event,i=u.checkStatus(f).data;s[t]&&s[t].call(this,new o(v,i))});u.on("tool("+f+")",function(n){var t=n.event;s[t]&&s[t].call(this,new o(v,[n.data]))});if(i.url){i.method="post";i.autoSort=!1;l=u.render(i);u.on("sort("+f+")",function(n){var i=t("#"+c).val();h.field=n.field;h.order=n.type;l.reload({initSort:n,where:h});t("#"+c).val(i)})}this.setData=function(n){i.data=n;l=u.render(i)};this.where={};this.reload=function(n){t.extend(this.where,n);h.query=JSON.stringify(this.where);l.reload({where:h})}}function o(n,t){function u(n,t,r){var u=n.rows.length,f=u>1?"所选的"+u+"条记录":"该记录";i.confirm("确定要删除"+f+"吗？",function(){i.post(t,{data:JSON.stringify(n.ids)},function(){n.grid.reload();r&&r(n)})})}this.grid=n;this.rows=t;this.row=t[0];this.selectRow=function(i){if(!t||t.length===0||t.length>1){r.msg("请选择一条记录！");return}var u=t[0];i&&i({grid:n,rows:t,row:u,ids:[u.Id]})};this.selectRows=function(i){if(!t||t.length===0){r.msg("请至少选择一条记录！");return}var u=[];t.forEach(function(n){u.push(n.Id)});i&&i({grid:n,rows:t,ids:u})};this.editRow=function(n,t){this.selectRow(function(i){n.show(i.row,t)})};this.deleteRow=function(n,t){this.selectRow(function(i){u(i,n,t)})};this.deleteRows=function(n,t){this.selectRows(function(i){u(i,n,t)})}}function h(n,i){this.form=n;this.elem=i;this.id=i[0].id;this.name=i[0].name;this.type=i[0].type;this.setReadonly=function(n){n?i.attr("readonly","readonly"):i.removeAttr("readonly")};this.getValue=function(){return this.type==="checkbox"?this.elem[0].checked?1:0:this.type==="radio"?t('input[name="'+this.name+'"]:checked').val():this.elem.val()};this.setValue=function(n){this.type==="checkbox"?this.elem[0].checked=n===1:this.type==="radio"?t('input[name="'+this.name+'"][value="'+n+'"]').attr("checked",!0):this.elem.val(n)}}function c(n){function nt(){v.length=0;var n=t("#"+o).find("input,select,textarea");t.each(n,function(n,i){var u=t(i),r;u.length&&(r=new h(f,u),r.setValue(""),p[u.attr("name")]=r,v.push(r))})}var o=n.name,u=n.config||{},y=n.toolbar,p=this,c=[],a={},b="",s,k,d,g,v,w;if(y)for(s=0;s<y.length;s++)k=y[s].text,c.push(k),d=s===0?"yes":"btn"+(s+1),b+='<a class="layui-btn layui-btn-normal" data-type="'+d+'">'+k+"<\/a>",g=y[s].handler,a[d]=function(){g&&g.call(this,new l(p))};if(a["btn"+(c.length+1)]=function(n){u.area?r.close(n):t("#"+o).hide()},c.push("关闭"),u.area)t.extend(u,{btn:c},a);else{b+='<a class="layui-btn layui-btn-primary" data-type="btn'+c.length+'">关闭<\/a>';t("#"+o+" .form-card-footer").html(b);t("#"+o+" .form-card-footer .layui-btn").on("click",function(){var n=t(this),i=n.data("type");a[i]?a[i].call(this,n):""})}v=[];w=0;this.show=function(r,s){var h,c,l,a;r=r||n.defData;h=n.title;h||(c=e(),h=c?c.title:"");s=s||{};l=s.title||"";a=r.Id===""?"【新增】":"【编辑】";l.indexOf("【")>-1&&(a="");h=h+a+l;u.area?(u.title=h,u.success=function(){f.render(null,o);nt();p.setData(r,u.init)},w=i.open(u)):(t("#"+o+" .form-card-header").html(h),nt(),this.setData(r,u.init),t("#"+o).show())};this.close=function(){w>0?r.close(w):t("#"+o).hide()};this.validate=function(){return!0};this.getData=function(){var n={};return t.each(v,function(t,i){n[i.name]=i.getValue()}),n};this.setData=function(n,t){var i,r,f;for(i in n)r=v.filter(function(n){return n.name===i}),r&&r.setValue(n[i]);f={form:p,data:n};u.setData&&u.setData(f);t&&t(f)}}function l(n){this.form=n;this.save=function(t,r){if(n.validate()){var u=n.getData();i.post(t,{data:JSON.stringify(u)},function(t){u.Id=t;n.setData(u);n.close();r&&r()})}}}function a(n,u){var f='<ul class="icon-list">';["heart-fill","heart","light","time","bluetooth","at","mute","mike","key","gift","email","rss","wifi","logout","android","ios","windows","transfer","service","subtraction","addition","slider","print","export","cols","screen-restore","screen-full","rate-half","rate","rate-solid","cellphone","vercode","login-wechat","login-qq","login-weibo","password","username","refresh-3","auz","spread-left","shrink-right","snowflake","tips","note","home","senior","refresh","refresh-1","flag","theme","notice","website","console","face-surprised","set","template-1","app","template","praise","tread","male","female","camera","camera-fill","more","more-vertical","rmb","dollar","diamond","fire","return","location","read","survey","face-smile","face-cry","cart-simple","cart","next","prev","upload-drag","upload","download-circle","component","file-b","user","find-fill","loading","loading-1","add-1","play","pause","headset","video","voice","speaker","fonts-del","fonts-code","fonts-html","fonts-strong","unlink","picture","link","face-smile-b","align-left","align-right","align-center","fonts-u","fonts-i","tabs","radio","circle","edit","share","delete","form","cellphone-fine","dialogue","fonts-clear","layer","date","water","code-circle","carousel","prev-circle","layouts","util","templeate-1","upload-circle","tree","table","chart","chart-screen","engine","triangle-d","triangle-r","file","set-sm","reduce-circle","add-circle","404","about","up","down","left","right","circle-dot","search","set-fill","group","friends","reply-fill","menu-fill","log","picture-fine","face-smile-fine","list","release","ok","help","chat","top","star","star-fill","close-fill","close","ok-circle","add-circle-fine"].forEach(function(t){var i="layui-icon-"+t;f+='<li id="'+i+'"';f+=i===n?' class="active">':">";f+='<i class="layui-icon '+i+'"><\/i>';f+="<\/li>"});f+="<\/ul>";i.open({title:"选择图标",area:["400px","250px"],content:f,btn:["确定","关闭"],yes:function(n){var i=t(".icon-list li.active").attr("id");r.close(n);u&&u(i)},btn2:function(n){r.close(n)},success:function(){t(".icon-list li").click(function(){t(".icon-list li").removeClass("active");t(this).addClass("active")})}})}var t=layui.jquery,r=layui.layer,f=layui.form,u=layui.table,i=layui.common;n("frame",{common:i,selectIcon:function(n,t){a(n,t)},grid:function(n){return new s(n)},form:function(n){return new c(n)},open:function(n){return i.open(n)},confirm:function(n,t){i.confirm(n,t)},post:function(n,t,r){i.post(n,t,r)}})});